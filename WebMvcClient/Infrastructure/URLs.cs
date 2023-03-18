namespace WebMvcClient.Infrastructure;

public static class URLs
{
    public static class Cars
    {
        public static string GetCarByIdUrl(string hostUrl, int carId)
        {
            return $"{hostUrl}/cars/{carId}";
        }

        public static string GetCarsRangeUrl(string hostUrl, int skip, int take)
        {
            return $"{hostUrl}/cars/{skip}:{take}";
        }

        public static string GetCarsUrl(string hostUrl)
        {
            return $"{hostUrl}/cars";
        }

        public static string PostCarUrl(string hostUrl)
        {
            return $"{hostUrl}/cars";
        }

        public static string UpdateCarUrl(string hostUrl)
        {
            return $"{hostUrl}/cars";
        }

        public static string DeleteCarUrl(string hostUrl, int carId)
        {
            return $"{hostUrl}/cars/{carId}";
        }
    }
}