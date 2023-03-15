using System;

namespace ArcCreate.Data
{
    public class ChartSettings
    {
        public string ChartPath { get; set; }

        public string AudioPath { get; set; }

        public string JacketPath { get; set; }

        public float BaseBpm { get; set; }

        public string BpmText { get; set; }

        public bool SyncBaseBpm { get; set; }

        public string BackgroundPath { get; set; }

        public string VideoPath { get; set; }

        public string Title { get; set; }

        public string Composer { get; set; }

        public string Charter { get; set; }

        public string Alias { get; set; }

        public string Illustrator { get; set; }

        public string Difficulty { get; set; }

        public double ChartConstant { get; set; }

        public string DifficultyColor { get; set; }

        public SkinSettings Skin { get; set; }

        public ColorSettings Colors { get; set; }

        public PoolSettings PoolSize { get; set; }

        public int LastWorkingTiming { get; set; }

        public int PreviewStart { get; set; }

        public int PreviewEnd { get; set; }

        public ChartSettings Clone()
        {
            return new ChartSettings()
            {
                Title = Title,
                Composer = Composer,
                Charter = Charter,
                Alias = Alias,
                Illustrator = Illustrator,
                BaseBpm = BaseBpm,
                BpmText = BpmText,
                SyncBaseBpm = SyncBaseBpm,
                Difficulty = Difficulty,
                ChartConstant = ChartConstant,
                DifficultyColor = DifficultyColor,
                Skin = Skin,
                Colors = Colors,
                LastWorkingTiming = LastWorkingTiming,
                AudioPath = AudioPath,
                JacketPath = JacketPath,
                BackgroundPath = BackgroundPath,
                VideoPath = VideoPath,
                PreviewStart = PreviewStart,
                PreviewEnd = PreviewEnd,
            };
        }

        public bool IsSameDifficulty(ChartSettings other)
        {
            if (other == null)
            {
                return false;
            }

            int thisRightDot = ChartPath.LastIndexOf('.');
            int otherRightDot = other.ChartPath.LastIndexOf('.');

            bool isSame = true;
            if (thisRightDot != otherRightDot)
            {
                isSame = false;
            }
            else
            {
                for (int i = 0; i < thisRightDot; i++)
                {
                    if (ChartPath[i] != other.ChartPath[i])
                    {
                        isSame = false;
                        break;
                    }
                }
            }

            if (isSame)
            {
                return true;
            }

            if (string.IsNullOrEmpty(Difficulty) || string.IsNullOrEmpty(other.Difficulty))
            {
                return false;
            }

            int thisRightSpace = Difficulty.LastIndexOf(' ');
            int otherRightSpace = other.Difficulty.LastIndexOf(' ');

            if (thisRightSpace == -1 || otherRightSpace == -1 || thisRightSpace != otherRightSpace)
            {
                return false;
            }

            for (int i = 0; i < thisRightSpace; i++)
            {
                if (Difficulty[i] != other.Difficulty[i])
                {
                    return false;
                }
            }

            return true;
        }

        public (int diff, bool isPlus) ParseChartConstant()
        {
            int roundDown = (int)ChartConstant;

            bool isPlus = roundDown >= 9 && (ChartConstant - roundDown) >= 0.7;

            return (roundDown, isPlus);
        }
    }
}