﻿#if NOUNITY
namespace DlkGames.Unity.TestHelpers.TestableUnity
#else
namespace UnityEngine
#endif
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Time
    {
        /// <summary>
        /// 
        /// </summary>
        public static float deltaTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public static float time
        {
            get;
            private set;
        }

        internal static void Update(float newTime)
        {
            deltaTime = newTime - time;
            time = newTime;
        }
    }
}