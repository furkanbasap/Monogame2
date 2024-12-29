using Microsoft.Xna.Framework;

namespace Monogame2.Managers
{
    public class AnimationManager
    {

        int numFrames;
        int numColumns;
        Vector2 size;

        int counter;
        int activeFrame;
        int interval;

        int rowPos;
        int colPos;

        private bool _active = true;

        public int OffsetX { get; set; } = 0;
        public int OffsetY { get; set; } = 0;

        public AnimationManager(int numFrames, int numColumns, Vector2 size)
        {
            this.numFrames = numFrames;
            this.numColumns = numColumns;
            this.size = size;

            counter = 0;
            activeFrame = 0;
            interval = 5;

        }

        public void Stop()
        {
            _active = false;
        }

        public void Start()
        {
            _active = true;
        }

        public void Update()
        {
            if (!_active) return;
            counter++;
            if (counter > interval)
            {
                counter = 0;
                NextFrame();
            }
        }

        private void NextFrame()
        {
            activeFrame++;
            colPos++;
            if (activeFrame >= numFrames)
            {
                ResetAnimation();
            }

            if (colPos >= numColumns)
            {
                colPos = 0;
                rowPos++;
            }
        }

        public Rectangle GetFrame()
        {
            return new Rectangle(
                colPos * (int)size.X + OffsetX,
                rowPos * (int)size.Y + OffsetY,
                (int)size.X,
                (int)size.Y);
        }

        private void ResetAnimation()
        {
            activeFrame = 0;
            colPos = 0;
            rowPos = 0;
        }
    }
}
