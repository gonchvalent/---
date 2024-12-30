using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AuthorApp
{
    public static class DataManager
    {
        private static readonly string CarsFilePath = "cars.json";
        private static readonly string VideosFilePath = "videos.json";

        public static Car GetArticleByBrandAndModel(string brand, string model)
        {
            var cars = GetAllCars();
            return cars.FirstOrDefault(car =>
                car.Brand.Equals(brand, System.StringComparison.OrdinalIgnoreCase) &&
                car.Model.Equals(model, System.StringComparison.OrdinalIgnoreCase));
        }

        public static List<Car> GetAllCars()
        {
            if (File.Exists(CarsFilePath))
            {
                var json = File.ReadAllText(CarsFilePath);
                return JsonSerializer.Deserialize<List<Car>>(json);
            }
            return new List<Car>();
        }

        public static void SaveCar(Car car)
        {
            var cars = GetAllCars();
            cars.Add(car);
            SaveCars(cars);
        }

        public static void UpdateCar(Car updatedCar)
        {
            var cars = GetAllCars();
            var index = cars.FindIndex(c => c.Brand == updatedCar.Brand && c.Model == updatedCar.Model);
            if (index != -1)
            {
                cars[index] = updatedCar;
                SaveCars(cars);
            }
        }

        public static void DeleteCar(Car car)
        {
            var cars = GetAllCars();
            cars.RemoveAll(c => c.Brand == car.Brand && c.Model == car.Model);
            SaveCars(cars);
        }

        private static void SaveCars(List<Car> cars)
        {
            var json = JsonSerializer.Serialize(cars, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(CarsFilePath, json);
        }

        public static List<Video> GetAllVideos()
        {
            if (File.Exists(VideosFilePath))
            {
                var json = File.ReadAllText(VideosFilePath);
                return JsonSerializer.Deserialize<List<Video>>(json);
            }
            return new List<Video>();
        }

        public static void SaveVideo(Video video)
        {
            var videos = GetAllVideos();
            videos.Add(video);
            SaveVideos(videos);
        }

        public static void UpdateVideo(Video updatedVideo)
        {
            var videos = GetAllVideos();
            var index = videos.FindIndex(v => v.Title == updatedVideo.Title);
            if (index != -1)
            {
                videos[index] = updatedVideo;
                SaveVideos(videos);
            }
        }

        public static void DeleteVideo(Video video)
        {
            var videos = GetAllVideos();
            videos.RemoveAll(v => v.Title == video.Title);
            SaveVideos(videos);
        }

        private static void SaveVideos(List<Video> videos)
        {
            var json = JsonSerializer.Serialize(videos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(VideosFilePath, json);
        }

        public static List<string> GetAllBrands()
        {
            var cars = GetAllCars();
            return cars.Select(c => c.Brand).Distinct().ToList();
        }

        public static List<string> GetModelsByBrand(string brand)
        {
            var cars = GetAllCars();
            return cars.Where(c => c.Brand == brand).Select(c => c.Model).ToList();
        }

        public static void DeleteArticle(string brand, string model)
        {
            DeleteCar(new Car { Brand = brand, Model = model });
        }

        public static void UpdateArticle(Car updatedArticle)
        {
            UpdateCar(updatedArticle);
        }

        public static Video GetVideoByTitle(string title)
        {
            var videos = GetAllVideos();
            return videos.FirstOrDefault(video => video.Title.Equals(title, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}