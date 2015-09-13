using System;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.IO;

namespace CampGoodtimesSpa.Models
{
    public class ImageFormatter : MediaTypeFormatter
    {
        private Type arrayType;

        public ImageFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/jpeg"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/jpg"));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/png"));
            this.arrayType = typeof(byte[]);
        }
        public override bool CanReadType(Type type)
        {
            return type == arrayType;
        }

        public override bool CanWriteType(Type type)
        {
            return type == arrayType;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            var taskSource = new TaskCompletionSource<object>();
            try
            {
                var ms = new MemoryStream();
                readStream.CopyTo(ms);
                taskSource.SetResult(ms.ToArray());
            }
            catch (Exception e)
            {
                taskSource.SetException(e);
            }
            return taskSource.Task;

        }
    }
}