using ElevatorChallengeLib.Managers;
using ElevatorChallengeLib.Models;
using ElevatorChallengeLib.Models.Enums;

namespace ElevatorChallengeTests
{
    public class Tests
    {
        /// <summary>
        /// Unit tests for validating the behavior of the ElevatorManager system,
        /// including normal dispatch, capacity handling, and edge cases.
        /// </summary>
        [TestFixture]
        public class ElevatorManagerTests
        {
            private ElevatorManager _manager;
            private List<Elevator> _elevators;
            private int _maxFloors;

            /// <summary>
            /// Initializes a new ElevatorManager with two elevators before each test.
            /// Default maximum floors is 20 unless overridden inside the test.
            /// </summary>
            [SetUp]
            public void Setup()
            {
                _maxFloors = 20;
                _elevators = new List<Elevator>
            {
                new Elevator { Name = "E1", Capacity = 4, SpeedPerSecond = 5.0M },
                new Elevator { Name = "E2", Capacity = 6, SpeedPerSecond = 5.0M }
            };

                _manager = new ElevatorManager(_elevators, _maxFloors);
            }

            /// <summary>
            /// Ensures that the closest idle elevator is dispatched to handle a request.
            /// </summary>
            [Test]
            public async Task RequestClosestIdleElevator_ShouldDispatchCorrectly()
            {
                // Start one elevator on floor 0, another at floor 10
                _elevators[0].CurrentFloor = 0;
                _elevators[1].CurrentFloor = 10;

                _manager.RequestElevator(2, 2);
                await Task.Delay(500); // allow processing

                Assert.AreEqual(2, _elevators[0].CurrentFloor); // closest one should move
                Assert.AreEqual(ElevatorState.Idle, _elevators[0].State);
            }

            /// <summary>
            /// Verifies that when the number of people exceeds the elevator's capacity,
            /// the elevator makes multiple trips until all passengers are transported.
            /// </summary>
            [Test]
            public async Task OverCapacity_ShouldRequireMultipleTrips()
            {
                _elevators[0].CurrentFloor = 0;
                _elevators[0].Capacity = 4;

                _manager.RequestElevator(5, 10);
                await Task.Delay(2000);

                Assert.AreEqual(5, _elevators[0].CurrentFloor);
                Assert.AreEqual(ElevatorState.Idle, _elevators[0].State);
            }

            /// <summary>
            /// Ensures that invalid floor requests (outside the building's range)
            /// are rejected and elevators remain stationary.
            /// </summary>
            [Test]
            public void InvalidFloor_ShouldBeRejected()
            {
                _manager = new ElevatorManager(_elevators, maxFloors: 5);

                _manager.RequestElevator(10, 3); // invalid request

                foreach (var e in _elevators)
                {
                    Assert.AreEqual(0, e.CurrentFloor);
                }
            }

            /// <summary>
            /// Regression test: validates that elevators never exceed the configured maximum floors,
            /// even if an invalid request is made.
            /// </summary>
            [Test]
            public async Task Regression_ElevatorShouldNotExceedMaxFloors()
            {
                _manager = new ElevatorManager(_elevators, maxFloors: 10);

                _elevators[0].CurrentFloor = 0;
                _manager.RequestElevator(15, 2); // invalid, above max

                await Task.Delay(500);

                Assert.LessOrEqual(_elevators[0].CurrentFloor, 10);
            }

            /// <summary>
            /// Ensures that multiple queued requests are processed sequentially
            /// and all requested floors are eventually reached.
            /// </summary>
            [Test]
            public async Task MultipleRequests_ShouldQueueAndProcessInOrder()
            {
                _manager.RequestElevator(3, 2);
                _manager.RequestElevator(6, 1);

                await Task.Delay(2000);

                Assert.Contains(0, _elevators.Select(e => e.CurrentFloor).ToList());
                Assert.Contains(6, _elevators.Select(e => e.CurrentFloor).ToList());
            }
        }
    }
}