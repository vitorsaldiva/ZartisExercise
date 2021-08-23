using System;
using System.Collections.Generic;
using ZartisExercise.Library.Interface;
using ZartisExercise.Library.Model;

namespace ZartisExercise.Library
{
    public class LandingService : ILandingService
    {
        private readonly Platform Platform;
        private (int? x, int? y) PreviousCoordinates;
        private List<Tuple<int, int>>  BlockedPoints;

        public LandingService(Platform platform)
        {
            Platform = platform ?? throw new ArgumentNullException(nameof(platform));
            BlockedPoints = new List<Tuple<int, int>>();
        }

        public string QueryLanding(int x, int y)
        {
            if (ValidateOutOfPlatform(x, y))
                return LandingStatus.OUT_OF_PLATFORM;
            else if (ValidateClash(x, y))
                return LandingStatus.CLASH;
            else
            {
                PreviousCoordinates = (x, y);
                BlockSlotsForLanding();
                return LandingStatus.OK_FOR_LANDING;
            }
        }

        private bool ValidateOutOfPlatform(int x, int y)
        {
            return (x < Platform?.InitialPosition.x
                || x > (Platform?.InitialPosition.x + Platform?.Size)
                || y < Platform?.InitialPosition.y
                || y > Platform?.InitialPosition.y + Platform?.Size);
        }

        private bool ValidateClash(int x, int y)
        {
            return (PreviousCoordinates.x != null && PreviousCoordinates.y != null) && BlockedPoints.Contains(Tuple.Create(x, y));
        }

        private void BlockSlotsForLanding()
        {
            for (int blockedY = PreviousCoordinates.y.Value - 1; blockedY <= PreviousCoordinates.y + 1; blockedY++)
            {
                for (int blockedX = PreviousCoordinates.x.Value - 1; blockedX <= PreviousCoordinates.x + 1; blockedX++)
                {
                    BlockedPoints.Add(Tuple.Create(blockedX, blockedY));
                }
            }
        }
    }
}
