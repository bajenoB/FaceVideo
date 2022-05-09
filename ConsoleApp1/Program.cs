using System;
using System.Threading;
using Amazon.Rekognition.Model;
namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            DetectFace();

        }
        public static void DetectFace()
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials("", "");//тут должен быть ключ от амазона но я снёс аккаунт по причине снятие денег 

            var rekognition = new Amazon.Rekognition.AmazonRekognitionClient(awsCreden‌tials, Amazon.RegionEndpoint.USEast1);
            var numb = 0;
            var res = rekognition.StartFaceDetectionAsync(new Amazon.Rekognition.Model.StartFaceDetectionRequest() { Video = new Amazon.Rekognition.Model.Video() { S3Object = new Amazon.Rekognition.Model.S3Object() { Bucket = "idfcbones", Name = "videoplayback.mp4" } } }).Result;
            GetFaceDetectionResponse result;
            do
            {
                Thread.Sleep(4000);
                result = rekognition.GetFaceDetectionAsync(new Amazon.Rekognition.Model.GetFaceDetectionRequest() { JobId = res.JobId }).Result;
                if (result.JobStatus == Amazon.Rekognition.VideoJobStatus.SUCCEEDED)
                {
                    break;
                }
            } while (true);

            result.Faces.ForEach(x => Console.WriteLine($"[{numb++}]Boundbox'X'-{x.Face.BoundingBox.Width}\nBoundbox'Y'-{x.Face.BoundingBox.Height}\n"));

        }
    }
}