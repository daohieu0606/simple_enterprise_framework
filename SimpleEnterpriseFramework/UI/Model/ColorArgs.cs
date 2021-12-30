namespace UI.Model
{
    /// <summary>
    /// Defines the <see cref="IColor" />.
    /// </summary>
    public class ColorArgs
    {
        /// <summary>
        /// Defines the a.
        /// </summary>
        internal byte a;

        /// <summary>
        /// Defines the r.
        /// </summary>
        internal byte r;

        /// <summary>
        /// Defines the g.
        /// </summary>
        internal byte g;

        /// <summary>
        /// Defines the b.
        /// </summary>
        internal byte b;

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class.
        /// </summary>
        /// <param name="_a">The _a<see cref="byte"/>.</param>
        /// <param name="_r">The _r<see cref="byte"/>.</param>
        /// <param name="_g">The _g<see cref="byte"/>.</param>
        /// <param name="_b">The _b<see cref="byte"/>.</param>
        public ColorArgs(byte _a, byte _r, byte _g, byte _b)
        {
            a = _a; r = _r; g = _r; b = _b;
        }
    }
}
