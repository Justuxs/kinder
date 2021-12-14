namespace kinder_app.Models
{
    public struct Dimensions
    {
        public int Length;
        public int Height;
        public int Width;

        public Dimensions(int length, int height, int width)
        {
            this.Length = length;
            this.Height = height;
            this.Width = width;
        }

        public override string ToString()
        {
            return Length.ToString() + ',' + Height.ToString() + ',' + Width.ToString();
        }
    } 
}