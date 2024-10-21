// https://www.techopedia.com/definition/22817/vector-programming#:~:text=A%20vector%2C%20in%20programming%2C%20is,of%20the%20same%20basic%20type //
namespace FRamework.handlers.types
{
    public class vector
    {
        public double X { get; set; }
        public double Y { get; private set; }
        public double Z { get; set; }

        public vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        private vector(vector other)
        {
            X = other.X;
            Y = other.Y;
            Z = other.Z;
        }

        private void scale(double factor)
        {
            X *= factor;
            Y *= factor;
            Z *= factor;
        }

        public static vector operator +(vector a, vector b)
        {
            return add(a, b);
        }

        public static double operator *(vector a, vector b)
        {
            return product(a, b);
        }

        public static vector operator *(double a, vector b)
        {
            vector v = new vector(b);
            v.scale(a);
            return v;
        }

        public static vector add(vector a, vector b)
        {
            double newX = a.X + b.X;
            double newY = a.Y + b.Y;
            double newZ = a.Z + b.Z;
            vector v = new vector(newX, newY, newZ);
            return v;
        }

        public static double product(vector a, vector b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public override string ToString()
        {
            return $"[{X}, {Y}, {Z}]";
        }
    }
}
