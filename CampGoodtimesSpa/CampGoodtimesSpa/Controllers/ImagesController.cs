using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CampGoodtimesSpa.Models.Images;
using System.IO;
using CampGoodtimesSpa.Models.Services;

namespace CampGoodtimesSpa.Models.Controllers
{
    public class ImagesController : BaseController
    {
        private const string ImagesSubdir = @"Content\images";
        public ImagesController(ISignInService signInService)
            : base(signInService)
        {

        }

        [HttpGet]
        public CampGoodtimesSpa.Models.Images.Folder GetRootFolder()
        {
            var rootPath = GetServerRootPath();
            var imageRoot = Path.Combine(rootPath, ImagesSubdir);
            List<FolderItem> items = new List<FolderItem>();
            var dirs = Directory.EnumerateDirectories(imageRoot);
            foreach (var dir in dirs)
            {
                items.Add(new FolderItem
                {
                    IsFolder = true,
                    Path = MakeWebDir(dir, rootPath),
                    Name = GetSubdirName(dir)
                });
            }

            var files = Directory.EnumerateFiles(imageRoot);
            foreach (var file in files)
            {
                items.Add(new FolderItem
                {
                    IsFolder = false,
                    Path = MakeWebDir(file, rootPath),
                    Name = Path.GetFileName(file)
                });
            }

            return new Folder
            {
                Items = items
            };
        }

        [HttpGet]
        public CampGoodtimesSpa.Models.Images.Folder GetFolderContents(string atPath)
        {
            var rootPath = GetServerRootPath();
            var fsSubDir = MakeFileSystemPath(atPath);
            var root = Path.Combine(rootPath, fsSubDir);
            List<FolderItem> items = new List<FolderItem>();
            var dirs = Directory.EnumerateDirectories(root);
            foreach (var dir in dirs)
            {
                items.Add(new FolderItem
                {
                    IsFolder = true,
                    Path = MakeWebDir(dir, rootPath),
                    Name = GetSubdirName(dir)
                });
            }

            var files = Directory.EnumerateFiles(root);
            foreach (var file in files)
            {
                items.Add(new FolderItem
                {
                    IsFolder = false,
                    Path = MakeWebDir(file, rootPath),
                    Name = Path.GetFileName(file)
                });
            }

            return new Folder
            {
                Items = items
            };
        }

        [HttpDelete]
        public HttpResponseMessage Delete(string atPath)
        {
            var rootPath = GetServerRootPath();
            var fsFilePath = MakeFileSystemPath(atPath);
            var fullPath = Path.Combine(rootPath, fsFilePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UploadImage(string id)
        {
            var rootPath = GetServerRootPath();
            var imageRoot = Path.Combine(rootPath, ImagesSubdir);

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var provider = new MultipartFormDataStreamProvider(imageRoot);

            // Read the form data and return an async task.
            await Request.Content.ReadAsMultipartAsync(provider);

            // This illustrates how to get the file names.
            foreach (MultipartFileData file in provider.FileData)
            {
                var fileName = file.Headers.ContentDisposition.FileName;
                var filePath = Path.Combine(Path.GetDirectoryName(file.LocalFileName), TrimName(fileName));

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                File.Move(file.LocalFileName, filePath);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}