namespace HelloWorld2
{
    public class Text : GameObject
    {
        public Text(int x, int y, int size, string value, string colour)
        {
            X = x;
            Y = y;
            Size = size;
            Value = value;
            Colour = colour;
        }

        public string Value { get; set; }
        public int Size { get; set; }
        public string Colour { get; set; }
    }
}
