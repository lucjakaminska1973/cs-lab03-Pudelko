using System;

namespace PudelkoLibrary
{
    public enum UnitOfMeasure 
    {
        meter,
        milimeter,
        centimeter
    }

    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>
    {
        private readonly double a;
        private readonly double b;
        private readonly double c;

        public UnitOfMeasure Unit{ get; set ;}

        public Pudelko(double a, double b, double c, UnitOfMeasure unit)
        {
            this.Unit = unit;
            this.a = GetValue(a, Unit);
            this.b = GetValue(b, Unit);
            this.c = GetValue(c, Unit);
        }

        public Pudelko(double a, double b, UnitOfMeasure unit)
        {
            this.Unit = unit;
            this.a = GetValue(a, Unit);
            this.b = GetValue(b, Unit);
            this.c = GetValue(0, Unit);
        }

        public Pudelko(double a, double b)
        {
            this.Unit = UnitOfMeasure.meter;
            this.a = GetValue(a, Unit);
            this.b = GetValue(b, Unit);
            this.c = GetValue(0, Unit);
        }

        public Pudelko(double a, UnitOfMeasure unit)
        {
            this.Unit = unit;
            this.a = GetValue(a, Unit);
            this.b = GetValue(0, Unit);
            this.c = GetValue(0, Unit);
        }

        public Pudelko(double a)
        {
            this.Unit = UnitOfMeasure.meter;
            this.a = GetValue(a, Unit);
            this.b = GetValue(0, Unit);
            this.c = GetValue(0, Unit);
        }

        public Pudelko(double a, double b, double c)
        {
            this.Unit = UnitOfMeasure.meter;
            this.a = GetValue(a, Unit);
            this.b = GetValue(b, Unit);
            this.c = GetValue(c, Unit);
        }

        public Pudelko() : this(10, 10, 10, UnitOfMeasure.centimeter) {}

        private double GetValue(double value, UnitOfMeasure unit)
        {
            if (value < 0) throw new ArgumentOutOfRangeException("Wartość nie może być mniejsza od 0");
            if (value == 0)
            {
                switch (unit)
                {
                    case UnitOfMeasure.centimeter:
                        return 10;
                    case UnitOfMeasure.meter:
                        return 0.1;
                    case UnitOfMeasure.milimeter:
                        return 100;
                    
                }
            }
            if ((unit == UnitOfMeasure.centimeter && value > 1000)
                || (unit == UnitOfMeasure.meter && value > 10)
                    || (unit == UnitOfMeasure.milimeter && value > 10_000))
            {
                throw new ArgumentException("Zbyt duża wartość");
            }
            else if (unit == UnitOfMeasure.centimeter)
            {
                return (double)Math.Round(value, 1);
            }
            else if (unit == UnitOfMeasure.meter)
            {
                return Math.Round(value, 3);
            }
            else if (unit == UnitOfMeasure.milimeter)
            {
                return (double)Math.Round(value);
            }
            else return 0.1;
        }

        //properties
        public double A
        {
            get
            {
                switch (Unit)
                {
                    case UnitOfMeasure.centimeter:
                        return Math.Round(a / 100, 3);
                    case UnitOfMeasure.meter:
                        return Math.Round(a, 3);
                    case UnitOfMeasure.milimeter:
                        return Math.Round(a / 1000, 3);
                    default:
                        return 0.1;
                }
            }
        }

        public double B
        {
            get
            {
                switch (Unit)
                {
                    case UnitOfMeasure.centimeter:
                        return Math.Round(b / 100, 3);
                    case UnitOfMeasure.meter:
                        return Math.Round(b, 3);
                    case UnitOfMeasure.milimeter:
                        return Math.Round(b / 1000, 3);
                    default:
                        return 0.1;
                }
            }
        }

        public double C
        {
            get
            {
                switch (Unit)
                {
                    case UnitOfMeasure.centimeter:
                        return Math.Round(c / 100, 3);
                    case UnitOfMeasure.meter:
                        return Math.Round(c, 3);
                    case UnitOfMeasure.milimeter:
                        return Math.Round(c / 1000, 3);
                    default:
                        return 0.1;
                }
            }
        }

        public override string ToString()
        {
            return this.ToString( "m", null);
        }

        public string ToString(string format) => this.ToString(format, null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "m":
                    string aAsMeter = string.Format("{0:0,000}", a);
                    string bAsMeter = string.Format("{0:0,000}", b);
                    string cAsMeter = string.Format("{0:0,000}", c);
                    return $"";
                case "cm":
                    string aAsCentimeter = string.Format("{0:0,0}", a);
                    string bAsCentimeter = string.Format("{0:0,0}", b);
                    string cAsCentimeter = string.Format("{0:0,0}", c);
                    return $"";
                case "mm":
                    int aAsMilimeter = (int)a;
                    int bAsMilimeter = (int)b;
                    int cAsMilimeter = (int)c;
                    return $"";
                default:
                    return "";

            }
        }

        //properties
        public double Objetosc => Math.Round(A * B * C, 6);

        public double Pole => Math.Round(A * B + A * C + B * C, 6);


        public bool Equals( Pudelko other)
        {
            if (other is null) return false;
            if (Object.ReferenceEquals(this, other)) return true;

            return (A == other.A) && (B == other.B) && (C == other.C);
        }

        public override bool Equals( object obj)
        {
            if (obj is Pudelko)
                return base.Equals((Pudelko)obj);
            else
                return false;
        }

        public override int GetHashCode() => (A, B, C).GetHashCode();
        
        public static bool Equals(Pudelko p1, Pudelko p2)
        {
            if ((p1 is null) && (p2 is null)) return true;
            if (p1 is null) return false;

            return p1.Equals(p2);
        }

        // przeciążenie operatorów == i !=

        public static bool operator ==(Pudelko p1, Pudelko p2) => Equals(p1, p2);
        public static bool operator !=(Pudelko p1, Pudelko p2) => !(p1 == p2);

        // dodawanie pudełek
        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            double temp1, temp2;
            double[] p1Tab = new double[] { p1.A, p1.B, p1.C };
            double[] p2Tab = new double[] { p2.A, p2.B, p2.C };
            Array.Sort(p1Tab);
            Array.Sort(p2Tab);

            if (p1Tab[1] >= p2Tab[1])
            {
                temp1 = p1Tab[1];
            }
            else
            {
                temp1 = p2Tab[1];
            }
            if (p1Tab[2] >= p2Tab[2])
            {
                temp2 = p1Tab[2];
            }
            else
            {
                temp2 = p2Tab[2];
            }
            return new Pudelko(p1Tab[0] + p2Tab[0], temp1, temp2);
        }
    }

}
