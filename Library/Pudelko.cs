using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace PudelkoLibrary
{
    public enum UnitOfMeasure
    {
        meter,
        milimeter,
        centimeter
    }

    public sealed class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable, IEnumerator
    {
        private readonly double a;
        private readonly double b;
        private readonly double c;

        public UnitOfMeasure unit { get; set; }

        protected double[] getSize;
        public double[] sizesAsMeter => getSize;

        public Pudelko(double a, double b, double c, UnitOfMeasure unit)
        {
            this.unit = unit;
            this.a = GetValue(a, unit);
            this.b = GetValue(b, unit);
            this.c = GetValue(c, unit);
            ToTab();
        }

        public Pudelko(double a, double b, double c)
        {
            this.unit = UnitOfMeasure.meter;
            this.a = GetValue(a, UnitOfMeasure.meter);
            this.b = GetValue(b, UnitOfMeasure.meter);
            this.c = GetValue(c, UnitOfMeasure.meter);
            ToTab();
        }

        public Pudelko(double a, double b, UnitOfMeasure unit)
        {

            this.unit = unit;
            this.a = GetValue(a, unit);
            this.b = GetValue(b, unit);
            this.c = ValueDefoult(unit);
            ToTab();
        }

        public Pudelko(double a, double b)
        {
            this.unit = UnitOfMeasure.meter;
            this.a = GetValue(a, unit);
            this.b = GetValue(b, unit);
            this.c = ValueDefoult(unit);
            ToTab();
        }

        public Pudelko(double a, UnitOfMeasure unit)
        {
            this.unit = unit;
            this.a = GetValue(a, unit);
            this.b = ValueDefoult(unit);
            this.c = ValueDefoult(unit);
            ToTab();
        }

        public Pudelko(double a)
        {
            this.unit = UnitOfMeasure.meter;
            this.a = GetValue(a, unit);
            this.b = ValueDefoult(unit);
            this.c = ValueDefoult(unit);
            ToTab();
        }

        

        public Pudelko() : this(10, 10, 10, UnitOfMeasure.centimeter) { ToTab(); }

        public void ToTab()
        {
            getSize = new double[] { A, B, C };
            
        }

        private double GetValue(double value, UnitOfMeasure unit)
        {
            double temp = 0;
            if ((value <= 0) || (unit == UnitOfMeasure.centimeter && value >= 1000)
                || (unit == UnitOfMeasure.meter && value >= 10)
                    || (unit == UnitOfMeasure.milimeter && value >= 10000))
            {
                throw new ArgumentOutOfRangeException("Wartość nie może być mniejsza od 0");
            }
            
            else if (unit == UnitOfMeasure.centimeter)
            {
                if (Math.Round(value, 1) > 0) temp = Math.Round(value, 1);
                else throw new ArgumentOutOfRangeException();
            }
            else if (unit == UnitOfMeasure.meter)
            {
                if (Math.Round(value, 3) > 0) temp = Math.Round(value, 3);
                else throw new ArgumentOutOfRangeException();
            }
            else if (unit == UnitOfMeasure.milimeter)
            {
                if (Math.Round(value) > 0) temp = Math.Round(value);
                else throw new ArgumentOutOfRangeException();
            }
            return temp;
            
        }

        private double ValueDefoult(UnitOfMeasure unit)
        {
            double temp = 0;
            switch (unit)
            {
                case UnitOfMeasure.centimeter:
                    temp = 10;
                    break;
                case UnitOfMeasure.meter:
                    temp = 0.1;
                    break;
                case UnitOfMeasure.milimeter:
                    temp = 100;
                    break;
                default:
                    temp = 0.1;
                    break;

            }
            return temp;
        }

        //properties
        public double A
        {
            get
            {
                double temp = 0;
                switch (unit)
                {
                    case UnitOfMeasure.centimeter:
                        temp =  Math.Round(a / 100, 3);
                            break;
                    case UnitOfMeasure.meter:
                        temp = Math.Round(a, 3);
                            break;
                    case UnitOfMeasure.milimeter:
                        temp = Math.Round(a / 1000, 3);
                            break;
                }

            return temp;
            }
        }

        public double B
        {
            get
            {
                double temp = 0;
                switch (unit)
                {
                    case UnitOfMeasure.centimeter:
                        temp = Math.Round(b / 100, 3);
                        break;
                    case UnitOfMeasure.meter:
                        temp = Math.Round(b, 3);
                        break;
                    case UnitOfMeasure.milimeter:
                        temp = Math.Round(b / 1000, 3);
                        break;

                }
                return temp;
            }
        }

        public double C
        {
            get
            {
                double temp = 0;
                switch (unit)
                {
                    case UnitOfMeasure.centimeter:
                        temp = Math.Round(c / 100, 3);
                        break;
                    case UnitOfMeasure.meter:
                        temp = Math.Round(c, 3);
                        break;
                    case UnitOfMeasure.milimeter:
                        temp = Math.Round(c / 1000, 3);
                        break;

                }
                return temp;
            }
        }

        public override string ToString()
        {
            return this.ToString("m", null);
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
        public double Objetosc => Math.Round(A * B * C, 9);

        public double Pole => Math.Round(A * B + A * C + B * C, 6);


        public bool Equals(Pudelko other)
        {
            if (other is null) return false;
            if (Object.ReferenceEquals(this, other)) return true;

            return (A == other.A) && (B == other.B) && (C == other.C);
        }

        public override bool Equals(object obj)
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
            double[] p1Tab = p1.sizesAsMeter;
            double[] p2Tab = p2.sizesAsMeter;
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



        public static implicit operator Pudelko(Tuple<int, int, int> tuple)
        {
            
            return new Pudelko((double)tuple.Item1, (double)tuple.Item2, (double)tuple.Item3, UnitOfMeasure.milimeter);
            
        }

        public static explicit operator double[](Pudelko P) => P.sizesAsMeter;


        public static double ShowItem(int i, Pudelko p1)
        {
            //double[] p1Tab = p1.sizesAsMeter;
            return p1.sizesAsMeter[i];
        }

        public Pudelko[] ShowItems(Pudelko p1)
        {
            var temp = new Pudelko[3];
            int i = 0;
            foreach (var x in p1)
            {
                temp[i] = x;
                i++;
            }
            return temp;
        }
        // implementacja Ienumerable
        private readonly Pudelko[] _size;
        public Pudelko(Pudelko[] pudelkoTab)
        {
            _size = new Pudelko[pudelkoTab.Length];
            for  (int i = 0; i< pudelkoTab.Length; i++)
            {
                _size[i] = pudelkoTab[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public Pudelko GetEnumerator()
        {
            return new Pudelko(_size);
        }
        
        int currentIndex = -1;

        public bool MoveNext()
        {
            currentIndex++;

            return (currentIndex < _size.Length);
        }

        object IEnumerator.Current => Current;

        public Pudelko Current
        {
            get
            {
                try
                {
                    return _size[currentIndex];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Reset()
        {
            currentIndex = -1;
        }




    }

}
