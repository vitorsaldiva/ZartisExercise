using System;
using Xunit;
using ZartisExercise.Library;
using ZartisExercise.Library.Interface;
using ZartisExercise.Library.Model;

namespace ZartisExercise.Test
{
    public class RocketLandingTests
    {
        private ILandingService landingService;
        public RocketLandingTests()
        {
        }

        [Theory]
        [InlineData(5, 5, 10, 1, 100, 101)]
        [InlineData(5, 5, 10, 1, 101, 100)]
        [InlineData(5, 5, 10, 1, 101, 101)]
        [InlineData(5, 5, 10, 1, -5, -5)]
        [InlineData(5, 5, 10, 1, -5, 5)]
        [InlineData(5, 5, 10, 1, 5, -5)]
        [InlineData(5, 5, 10, 1, 16, 15)]
        [InlineData(5, 5, 10, 1, 15, 16)]
        [InlineData(5, 5, 10, 1, 4, 4)]
        [InlineData(5, 5, 10, 1, 4, 5)]
        [InlineData(5, 5, 10, 1, 5, 4)]
        public void QueryLanding_OutOfPlatformStatus(int initialX, int initialY, int platformSize, int platformUnit, int x, int y)
        {
            var landingPlatform = new Platform((initialX, initialY), platformSize, platformUnit);
            landingService = new LandingService(landingPlatform);

            string slotStatus = landingService.QueryLanding(x, y);

            Assert.Equal(LandingStatus.OUT_OF_PLATFORM, slotStatus);
        }

        [Theory]
        [InlineData(5, 5, 10, 1, 5, 5)]
        [InlineData(5, 5, 10, 1, 7, 5)]
        [InlineData(5, 5, 10, 1, 10, 10)]
        public void QueryLanding_NoPreviousLanding_OkForLanding(int initialX, int initialY, int platformSize, int platformUnit, int x, int y)
        {
            var landingPlatform = new Platform((initialX, initialY), platformSize, platformUnit);
            landingService = new LandingService(landingPlatform);

            string slotStatus = landingService.QueryLanding(x, y);

            Assert.Equal(LandingStatus.OK_FOR_LANDING, slotStatus);
        }

        [Theory]
        [InlineData(5, 5, 10, 1, 7, 12, 5, 10)]
        [InlineData(5, 5, 10, 1, 7, 12, 5, 11)]
        [InlineData(5, 5, 10, 1, 7, 12, 5, 12)]
        [InlineData(5, 5, 10, 1, 7, 12, 5, 13)]
        [InlineData(5, 5, 10, 1, 7, 12, 5, 14)]
        [InlineData(5, 5, 10, 1, 7, 12, 6, 10)]
        [InlineData(5, 5, 10, 1, 7, 12, 7, 10)]
        [InlineData(5, 5, 10, 1, 7, 12, 8, 10)]
        [InlineData(5, 5, 10, 1, 7, 12, 6, 14)]
        [InlineData(5, 5, 10, 1, 7, 12, 7, 14)]
        [InlineData(5, 5, 10, 1, 7, 12, 8, 14)]
        [InlineData(5, 5, 10, 1, 7, 12, 9, 10)]
        [InlineData(5, 5, 10, 1, 7, 12, 9, 11)]
        [InlineData(5, 5, 10, 1, 7, 12, 9, 12)]
        [InlineData(5, 5, 10, 1, 7, 12, 9, 13)]
        [InlineData(5, 5, 10, 1, 7, 12, 9, 14)]
        public void QueryLanding_WithPreviousLanding_OkForLanding(int initialX, int initialY, int platformSize, int platformUnit, int prevX, int prevY, int x, int y)
        {
            var landingPlatform = new Platform((initialX, initialY), platformSize, platformUnit);
            landingService = new LandingService(landingPlatform);

            var previousRocketQuery = landingService.QueryLanding(prevX, prevY);
            string slotStatus = landingService.QueryLanding(x, y);

            Assert.Equal(LandingStatus.OK_FOR_LANDING, slotStatus);
        }

        [Theory]
        [InlineData(5, 5, 10, 1, 7, 12, 7, 12)]
        [InlineData(5, 5, 10, 1, 7, 12, 6, 11)]
        [InlineData(5, 5, 10, 1, 7, 12, 8, 13)]
        [InlineData(5, 5, 10, 1, 5, 5, 6, 5)]
        [InlineData(5, 5, 10, 1, 5, 5, 6, 6)]
        [InlineData(5, 5, 10, 1, 14, 14, 13, 13)]
        [InlineData(5, 5, 10, 1, 5, 14, 5, 13)]
        [InlineData(5, 5, 10, 1, 5, 14, 6, 13)]
        [InlineData(5, 5, 10, 1, 5, 14, 6, 14)]
        [InlineData(5, 5, 10, 1, 5, 9, 5, 8)]
        [InlineData(5, 5, 10, 1, 5, 9, 5, 10)]
        [InlineData(5, 5, 10, 1, 5, 9, 6, 8)]
        [InlineData(5, 5, 10, 1, 5, 9, 6, 9)]
        [InlineData(5, 5, 10, 1, 5, 9, 6, 10)]
        [InlineData(5, 5, 10, 1, 11, 9, 12, 9)]
        public void QueryLanding_WithPreviousLanding_Clash(int initialX, int initialY, int platformSize, int platformUnit, int prevX, int prevY, int x, int y)
        {
            var landingPlatform = new Platform((initialX, initialY), platformSize, platformUnit);
            landingService = new LandingService(landingPlatform);

            var previousRocketQuery = landingService.QueryLanding(prevX, prevY);
            string slotStatus = landingService.QueryLanding(x, y);

            Assert.Equal(LandingStatus.CLASH, slotStatus);
        }
    }
}
