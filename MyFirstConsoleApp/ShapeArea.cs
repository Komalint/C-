using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstConsoleApp
{
    public class ShapeArea
    {
        public string Shape_name { get; set; }

        public ShapeArea(string s)
        {
            this.Shape_name = s;
        }
        public  virtual void Draw(string shapeN)
        {
            Console.WriteLine($"Drawing the shape : {Shape_name}");
        }
    }

    public class ShapeAreaCal : ShapeArea
    {
        public int Length { get; set; }
        public int Breadth { get; set; }

        public int Height { get; set; }
        public ShapeAreaCal(string s , int l ,int b, int h): base(s) { }
        public override void Draw(string shapeN) {
            Console.WriteLine($"Shape Drawn : {shapeN}");
        }

        public int Area(int l, int b)
        {
            return l * b;
        }
        public int Area(int l, int b, int h) {
            return l * b * h;

        } 
    }

}
