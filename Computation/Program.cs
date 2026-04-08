using Shape;
namespace Computation;

class Program
{
    static void Main(string[] args)
    {
        //Tetrahedon
        Tetrahedon tet1 = new Tetrahedon();
        int[] centtet = tet1.Centroid();
        Console.WriteLine("Tet1: ");
        Console.WriteLine($"A= ({tet1.VerticeA[0]}|{tet1.VerticeA[1]}|{tet1.VerticeA[2]})");
        Console.WriteLine($"B= ({tet1.VerticeB[0]}|{tet1.VerticeB[1]}|{tet1.VerticeB[2]})");
        Console.WriteLine($"C= ({tet1.VerticeC[0]}|{tet1.VerticeC[1]}|{tet1.VerticeC[2]})");
        Console.WriteLine($"D= ({tet1.VerticeD[0]}|{tet1.VerticeD[1]}|{tet1.VerticeD[2]})");
        Console.WriteLine($"Surface-Area: {tet1.SurfaceArea()}, Cenroid: ({centtet[0]}|{centtet[1]}|{centtet[2]})");

        Cuboid cub1 = new Cuboid();
        int[] centcub = cub1.Centroid();
        Console.WriteLine("Cub1: ");
        Console.WriteLine($"A= ({cub1.VerticeA[0]}|{cub1.VerticeA[1]}|{cub1.VerticeA[2]})");
        Console.WriteLine($"B= ({cub1.VerticeB[0]}|{cub1.VerticeB[1]}|{cub1.VerticeB[2]})");
        Console.WriteLine($"C= ({cub1.VerticeC[0]}|{cub1.VerticeC[1]}|{cub1.VerticeC[2]})");
        Console.WriteLine($"D= ({cub1.VerticeD[0]}|{cub1.VerticeD[1]}|{cub1.VerticeD[2]})");
        Console.WriteLine($"E= ({cub1.VerticeE[0]}|{cub1.VerticeE[1]}|{cub1.VerticeE[2]})");
        Console.WriteLine($"F= ({cub1.VerticeF[0]}|{cub1.VerticeF[1]}|{cub1.VerticeF[2]})");
        Console.WriteLine($"G= ({cub1.VerticeG[0]}|{cub1.VerticeG[1]}|{cub1.VerticeG[2]})");
        Console.WriteLine($"H= ({cub1.VerticeH[0]}|{cub1.VerticeH[1]}|{cub1.VerticeH[2]})");
        Console.WriteLine($"Surface-Area: {cub1.SurfaceArea()}, Volume: {cub1.Volume()}, Cenroid: ({centcub[0]}|{centcub[1]}|{centcub[2]})");

        Cylinder cyl1 = new Cylinder();
        Console.WriteLine("Cyl1: ");
        Console.WriteLine($"A= ({cyl1.VerticeA[0]}|{cyl1.VerticeA[1]}|{cyl1.VerticeA[2]})");
        Console.WriteLine($"B= ({cyl1.VerticeB[0]}|{cyl1.VerticeB[1]}|{cyl1.VerticeB[2]})");
        Console.WriteLine($"Radius: {cyl1.radius}");
        Console.WriteLine($"Surface-Area: {cyl1.SurfaceArea()}, Bottom-Area: {cyl1.BottomArea()}, Volume: {cyl1.Volume()}, Height: {cyl1.calcHeight()}");
        
        CompoundShape comp = cub1 + tet1;
        Console.WriteLine("CpumpoundShap of Tet1 & Cub1: ");
        Console.WriteLine($"Compound Surface-Area: {comp.SurfaceArea()}, Compound Volume: {comp.Volume()}");
        Console.WriteLine(comp.IsIn(tet1));
        comp.Shapes.Sort();
        Console.WriteLine(comp.IsIn(tet1));
        
        Tetrahedon tet2 = new Tetrahedon(comp[0] as Tetrahedon);
        int[] centtet2 = tet2.Centroid();
        Console.WriteLine("Tet2: ");
        Console.WriteLine($"A= ({tet2.VerticeA[0]}|{tet2.VerticeA[1]}|{tet2.VerticeA[2]})");
        Console.WriteLine($"B= ({tet2.VerticeB[0]}|{tet2.VerticeB[1]}|{tet2.VerticeB[2]})");
        Console.WriteLine($"C= ({tet2.VerticeC[0]}|{tet2.VerticeC[1]}|{tet2.VerticeC[2]})");
        Console.WriteLine($"D= ({tet2.VerticeD[0]}|{tet2.VerticeD[1]}|{tet2.VerticeD[2]})");
        Console.WriteLine($"Surface-Area: {tet2.SurfaceArea()}, Cenroid: ({centtet2[0]}|{centtet2[1]}|{centtet2[2]})");
    }
}
