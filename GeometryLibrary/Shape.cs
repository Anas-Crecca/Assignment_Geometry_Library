using System.Reflection.Metadata.Ecma335;

namespace Shape;

public abstract class Shape : IComparable
{
 //Methods
 public virtual int CompareTo(object? obj)
    {
        if (obj == null) return 1;
        Shape otherShape = obj as Shape;
        if(otherShape != null)
        {
            return this.SurfaceArea().CompareTo(otherShape.SurfaceArea());
        }
        else
        {
          throw new ArgumentException("Object is not a Shape");  
        }
    }
 public virtual double Volume()
    {
        double Volume = 0;
        return Volume;
    }
 public virtual double SurfaceArea()
    {
        double Surface = 0;
        return Surface;
    }
public double calcSide(int[] Vertice1, int[] Vertice2)
    {
        //calc distance between vertices
       double side =  Math.Sqrt(
                    Math.Pow((Vertice2[0] - Vertice1[0]), 2) 
                    + Math.Pow((Vertice2[1] - Vertice1[1]), 2) 
                    + Math.Pow((Vertice2[2] - Vertice1[2]), 2)
                    );
        return side;
    }
 public static CompoundShape operator +(Shape shape1, Shape shape2)
    {
        CompoundShape comp = new CompoundShape(shape1,shape2);
        return comp;
    }

}
public class CompoundShape : Shape
{
    //Fields
    public List<Shape> Shapes = new List<Shape>();

    //Constructors
    public CompoundShape(Shape shape1, Shape shape2)
    {
        Shapes.Add(shape1);
        Shapes.Add(shape2);
    }
    public CompoundShape(int n)
    {
        var rand = new Random();
        for(int i = 0; i < n; i++)
        {
           int shape = rand.Next(0,4); 
           if(shape == 1)
            {
                Shapes.Add(new Tetrahedon());
            }else if(shape == 2)
            {
                Shapes.Add(new Cuboid());
            }
            else
            {
                Shapes.Add(new Cylinder());
            }
        }
    }


    //Methods
    public Shape this[int index]
    {
        get
        {
            return Shapes[index];
        }
        private set
        {
            Shapes[index] = value;
        }
    }
    public int IsIn(Shape s)
    {
        int index = 0;
        bool exists = false;
        for(int i = 0; i < Shapes.Count; i ++)
        {
            if(Shapes[i] == s)
            {
                exists = true;
                index = i;
            }
        }
        if (exists)
        {
            return index;
        }
        else
        {
            throw new ApplicationException("Shape is not in CompoundShape");
        }
        
        
    }
    public override double SurfaceArea()
    {
        double TotalSurface = 0;
       foreach (Shape i in Shapes){
           TotalSurface +=  i.SurfaceArea();
        }
        return TotalSurface;
    }
    public override double Volume()
    {
        double TotalVolume = 0;
       foreach (Shape i in Shapes){
           TotalVolume +=  i.Volume();
        }
        return TotalVolume;
    }

}
public class Tetrahedon : Shape
{
    //Fields
    public int[] VerticeA = new int[3];
    public int[] VerticeB = new int[3];
    public int[] VerticeC = new int[3];
    public int[] VerticeD = new int[3];

    //Constructor
    public Tetrahedon()
    {
        var rand = new Random();
        for(int i = 0; i < 3; i++)
        {
            VerticeA[i] = rand.Next(0, 100);
            VerticeB[i] = rand.Next(0, 100);
            VerticeC[i] = rand.Next(0, 100);
            VerticeD[i] = rand.Next(0, 100);
        }
    }
    public Tetrahedon(Tetrahedon tetrahedon)
    {
        for(int i = 0; i < 3; i++)
        {
            this.VerticeA[i] = tetrahedon.VerticeA[i];
            this.VerticeB[i] = tetrahedon.VerticeB[i];
            this.VerticeC[i] = tetrahedon.VerticeC[i];
            this.VerticeD[i] = tetrahedon.VerticeD[i];
        }
    }
    //Methods
    public int[] Centroid()
    {
        int[] centroid = new int[3];
        for(int i = 0; i < 3; i++)
        {
            centroid[i] = (VerticeA[i] + VerticeB[i] + VerticeC[i] + VerticeD[i])/4;
        }
        return centroid;
    }
    public override double SurfaceArea()
    {
        //ABC
        double tri1 = calcTriangle(VerticeA, VerticeB, VerticeC);
        //ABD
        double tri2 = calcTriangle(VerticeA, VerticeB, VerticeD);
        //ACD
        double tri3 = calcTriangle(VerticeA, VerticeC, VerticeD);
        //BCD
        double tri4 = calcTriangle(VerticeB, VerticeC, VerticeD);
        double Surface = tri1 + tri2 + tri3 + tri4;
        return Surface;
    }
    
    private double calcTriangle(int[] vert1, int[] vert2, int[] vert3)
    {
        double side1 = calcSide(vert1, vert2);
        double side2 = calcSide(vert2, vert3);
        double side3 = calcSide(vert3, vert1);
        double semiperimeter = (side1 + side2 + side3)/2;
        //calc area of trinagle using Heron formula
        double triangle = Math.Sqrt(semiperimeter * (semiperimeter - side1) * (semiperimeter - side2) * (semiperimeter - side3));
        return triangle;
    }
    //i dont know if this is what is meant:
    public static bool operator ==(Tetrahedon a, Tetrahedon b)
    {
        bool same = true;
        for(int i = 0; i < 3; i++)
        {
            if(a.VerticeA[i] != b.VerticeA[i] 
            ||a.VerticeB[i] != b.VerticeB[i]
            ||a.VerticeC[i] != b.VerticeC[i]
            ||a.VerticeD[i] != b.VerticeD[i])
            {
                same = false;
            }
        }
        return same;
    }
    public static bool operator !=(Tetrahedon a, Tetrahedon b)
    {
        bool diff = false;
        for(int i = 0; i < 3; i++)
        {
            if(a.VerticeA[i] != b.VerticeA[i] 
            ||a.VerticeB[i] != b.VerticeB[i]
            ||a.VerticeC[i] != b.VerticeC[i]
            ||a.VerticeD[i] != b.VerticeD[i])
            {
                diff = true;
            }
    }
    return diff;
}
}
public class Cuboid : Shape
{
     //Fields
    public int[] VerticeA = new int[3];
    public int[] VerticeB = new int[3];
    public int[] VerticeC = new int[3];
    public int[] VerticeD = new int[3];
    public int[] VerticeE = new int[3];
    public int[] VerticeF = new int[3];
    public int[] VerticeG = new int[3];
    public int[] VerticeH = new int[3];

    //Constructor
    public Cuboid()
    {
        var rand = new Random();
        //random shift for x,y and z
        int xShift = rand.Next(0,100);
        int yShift = rand.Next(0,100);
        int zShift = rand.Next(0,100);
        //assign random values for x, y and z coordinates of start
        for(int i = 0; i < 3; i++)
        {
            int start = rand.Next(0,100);
            VerticeA[i] = start;
            VerticeB[i] = start;
            VerticeC[i] = start;
            VerticeD[i] = start;
            VerticeE[i] = start;
            VerticeF[i] = start;
            VerticeG[i] = start;
            VerticeH[i] = start;
        }
        //assign x,y,z shift to vertices
        VerticeB[0] += xShift;

        VerticeC[2] += zShift;

        VerticeD[0] += xShift;
        VerticeD[2] += zShift;

        VerticeE[1] += yShift;

        VerticeF[0] += xShift;
        VerticeF[1] += yShift;

        VerticeG[1] += yShift;
        VerticeG[2] += zShift;

        VerticeH[0] += xShift;
        VerticeH[1] += yShift;
        VerticeH[2] += zShift;
    }
    public Cuboid(Cuboid cuboid)
    {
        for(int i = 0; i < 3; i++)
        {
            this.VerticeA[i] = cuboid.VerticeA[i];
            this.VerticeB[i] = cuboid.VerticeB[i];
            this.VerticeC[i] = cuboid.VerticeC[i];
            this.VerticeD[i] = cuboid.VerticeD[i];
            this.VerticeE[i] = cuboid.VerticeE[i];
            this.VerticeF[i] = cuboid.VerticeF[i];
            this.VerticeG[i] = cuboid.VerticeG[i];
            this.VerticeH[i] = cuboid.VerticeH[i];
        }
    }

    //Methods
    public int[] Centroid()
    {
        double lenght = calcSide(VerticeA,VerticeE);
        double width = calcSide(VerticeA, VerticeB);
        double height = calcSide(VerticeA, VerticeC);
        int[] Centroid = new int[]{(int) lenght/2, (int) width/2, (int) height/2};
        return Centroid;
    }
    public override double SurfaceArea()
    {
        double lenght = calcSide(VerticeA,VerticeE);
        double width = calcSide(VerticeA, VerticeB);
        double height = calcSide(VerticeA, VerticeC);
        double Surface = 2*(lenght*width + width*height + height*lenght);
        return Surface;
    }
    
    public override double Volume()
    {
        double lenght = calcSide(VerticeA,VerticeE);
        double width = calcSide(VerticeA, VerticeB);
        double height = calcSide(VerticeA, VerticeC);
        double volume = lenght * width * height;
        return volume;
    }
    public static bool operator ==(Cuboid a, Cuboid b)
    {
        bool same = true;
        for(int i = 0; i < 3; i++)
        {
            if(a.VerticeA[i] != b.VerticeA[i] 
            ||a.VerticeB[i] != b.VerticeB[i]
            ||a.VerticeC[i] != b.VerticeC[i]
            ||a.VerticeD[i] != b.VerticeD[i]
            ||a.VerticeE[i] != b.VerticeE[i]
            ||a.VerticeF[i] != b.VerticeF[i]
            ||a.VerticeG[i] != b.VerticeG[i]
            ||a.VerticeH[i] != b.VerticeH[i])
            {
                same = false;
            }
        }
        return same;
    }
    public static bool operator !=(Cuboid a, Cuboid b)
    {
        bool diff = false;
        for(int i = 0; i < 3; i++)
        {
            if(a.VerticeA[i] != b.VerticeA[i] 
            ||a.VerticeB[i] != b.VerticeB[i]
            ||a.VerticeC[i] != b.VerticeC[i]
            ||a.VerticeD[i] != b.VerticeD[i]
            ||a.VerticeE[i] != b.VerticeE[i]
            ||a.VerticeF[i] != b.VerticeF[i]
            ||a.VerticeG[i] != b.VerticeG[i]
            ||a.VerticeH[i] != b.VerticeH[i])
            {
                diff = true;
            }
        }
        return diff;
    }
}
public class Cylinder : Shape
{
    //Fields
    public int[] VerticeA = new int[3];
    public int[] VerticeB = new int[3];
    public int radius{get; private set;}

    //Constructor
    public Cylinder()
    {
        var rand = new Random();
        radius = rand.Next(0,100);
        for(int i = 0; i < 3; i++)
        {
            VerticeA[i] = rand.Next(0, 100);
            VerticeB[i] = rand.Next(0, 100);
        }
    }
    public Cylinder(Cylinder cylinder)
    {
        this.radius = cylinder.radius;
        for(int i = 0; i < 3; i++)
        {
            this.VerticeA[i] = cylinder.VerticeA[i];
            this.VerticeB[i] = cylinder.VerticeB[i];
        }
    }

    //Methods
    public double calcHeight()
    {
        double height = calcSide(VerticeA,VerticeB);
        return height;
    }
    public double BottomArea()
    {
        double area = Math.PI * Math.Pow(radius,2);
        return area;
    }
    public override double SurfaceArea()
    {
        double height = calcHeight();
        double bottom = BottomArea();
        double Surface = 2 * Math.PI * radius * height + 2 * bottom;
        return Surface;
    }
    public override double Volume()
    {
        double height = calcHeight();
        double volume = Math.PI * Math.Pow(radius,2) * height;
        return volume;
    }
    public static bool operator ==(Cylinder a, Cylinder b)
    {
        bool same = true;
        if(a.radius != b.radius)
        {
            same= false;
        }
        for(int i = 0; i < 3; i++)
        {
            if(a.VerticeA[i] != b.VerticeA[i] 
            ||a.VerticeB[i] != b.VerticeB[i])
            {
                same = false;
            }
        }
        return same;
    }
    public static bool operator !=(Cylinder a, Cylinder b)
    {
        bool diff = false;
        if(a.radius != b.radius)
        {
            diff= true;
        }
        for(int i = 0; i < 3; i++)
        {
            if(a.VerticeA[i] != b.VerticeA[i] 
            ||a.VerticeB[i] != b.VerticeB[i])
            {
                diff = true;
            }
        }
        return diff;
    }
}
