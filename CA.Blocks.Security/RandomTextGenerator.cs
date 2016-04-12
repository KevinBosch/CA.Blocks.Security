using System;
using System.Text;

namespace CA.Blocks.Security
{
    /// <summary>
    /// Generates random text.
    /// </summary>
    public class RandomTextGenerator : IRandomTextGenerator
    {
        /// <summary>
        /// Settings for the random text generator.
        /// </summary>
        public class RandomTextGeneratorSettings
        {
            public RandomTextGeneratorSettings()
            {
                AllowedChars = "abcdefghijklmnopqrstuvxyzABCDEFGHIJLKLMNOPQRSTUVXYZ0123456789";
                Length = 5;
            }

            /// <summary>
            /// Gets or sets the length of random charachters to generate
            /// </summary>
            /// <value>The length.</value>
            public int Length { get; set; }


            /// <summary>
            /// Gets or sets the allowed chars.
            /// </summary>
            /// <value>The allowed chars.</value>
            public string AllowedChars { get; set; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomTextGenerator"/> class.
        /// </summary>
        public RandomTextGenerator()
        {
            Settings = new RandomTextGeneratorSettings();
            Settings.Length = 4;
            // we take out common letters eg 8 B or 0 O or 1 I  this avoids confusion 
            Settings.AllowedChars = "ABCDEFGHJKLMNPRSTUVWXY3456789";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomTextGenerator"/> class.
        /// </summary>
        public RandomTextGenerator(int length)
        {
            Settings = new RandomTextGeneratorSettings();
            Settings.Length = length;
            // we take out common letters eg 8 B or 0 O or 1 I  this avoids confusion 
            Settings.AllowedChars = "ABCDEFGHJKLMNPRSTUVWXY3456789";
        }

        public RandomTextGenerator(RandomTextGeneratorSettings config)
        {
            Settings = config;
        }


        public RandomTextGeneratorSettings Settings { get; private set; }

        #region IRandomTextGenerator Members
        /// <summary>
        /// Generate the random text.
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            StringBuilder sb = new StringBuilder();
            int maxNumberOfChars = Settings.AllowedChars.Length - 1;
            for (int i = 0; i < Settings.Length; i++)
            {
                sb.Append(Settings.AllowedChars[rand.Next(0, maxNumberOfChars)]);
            }
            return sb.ToString();
        }
        #endregion
    }
}
