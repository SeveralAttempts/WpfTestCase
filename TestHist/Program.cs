using OpenCvSharp;

internal class Program
{
    private static void Main(string[] args)
    {
        string file = "C:\\Users\\coder7\\Downloads\\1_iYqZztXZhPBHQGZU1I0AzA.png";
        Mat src = new Mat(file);
        Mat gray = new Mat(file, ImreadModes.Grayscale);
        Mat hist = GetHistogram(gray);
        using (new Window("src image", src))
        using (new Window("gray image", gray))
        using (new Window("hist", hist))
        {
            Cv2.WaitKey();
        }
    }

    public static Mat GetHistogram(Mat source)
    {
        Mat hist = new Mat();
        int width = source.Cols, height = source.Rows;      // set Histogram same size as source image
        const int histogramSize = 256;                      // you can change by urself
        int[] dimensions = { histogramSize };               // Histogram size for each dimension
        Rangef[] ranges = { new Rangef(0, histogramSize) }; // min/max

        Cv2.CalcHist(
            images: new[] { source },
            channels: new[] { 0 }, //The channel (dim) to be measured. In this case it is just the intensity (each array is single-channel) so we just write 0.
            mask: null,
            hist: hist,
            dims: 1, //The histogram dimensionality.
            histSize: dimensions,
            ranges: ranges);

        hist.his
        Mat render = new Mat(new Size(width, height), MatType.CV_8UC3, Scalar.All(255));
        double minVal, maxVal;
        Cv2.MinMaxLoc(hist, out minVal, out maxVal);
        Scalar color = Scalar.All(100);
        // Scales and draws histogram
        hist = hist * (maxVal != 0 ? height / maxVal : 0.0);
        int binW = width / dimensions[0];
        for (int j = 0; j < dimensions[0]; ++j)
        {
            Console.WriteLine($@"j:{j} P1: {j * binW},{render.Rows} P2:{(j + 1) * binW},{render.Rows - hist.Get<int>(j)}");  //for Debug
        }
        return render;
    }
}