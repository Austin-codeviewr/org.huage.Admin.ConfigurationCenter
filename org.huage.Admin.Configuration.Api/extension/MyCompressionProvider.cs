using Microsoft.AspNetCore.ResponseCompression;

namespace org.huage.Admin.Configuration.Api.extension;

public class MyCompressionProvider : ICompressionProvider
{
    public Stream CreateStream(Stream outputStream)
    {
        StreamReader stream = new StreamReader(new MemoryStream());
        stream.ReadToEndAsync();
        return new MemoryStream();
    }

    public string EncodingName { get; } = "myC";

    /// <inheritdoc />
    public bool SupportsFlush => true;
}