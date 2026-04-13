namespace Clean_Architecture_Sample.Application.Helpers;

public static class ImageFilePathHelper
{
    public static string? NormalizeFileName(string? imageFile)
    {
        if (string.IsNullOrWhiteSpace(imageFile))
        {
            return null;
        }

        return Path.GetFileName(imageFile.Trim());
    }

    public static string? ToClientPath(string? imageFile)
    {
        var fileName = NormalizeFileName(imageFile);
        return fileName is null ? null : $"/Images/{fileName}";
    }
}
