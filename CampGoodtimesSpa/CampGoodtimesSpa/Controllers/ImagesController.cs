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

namespace CampGoodtimesSpa.Models.Controllers
{
    public class ImagesController : ApiController
    {
        private const string ImagesSubdir = @"Content\images";
        public ImagesController()
        {

        }

        [HttpGet]
        public CampGoodtimesSpa.Models.Images.Folder GetRootFolder()
        {
            var rootPath = GetServerRootPath();
            var imageRoot = Path.Combine(rootPath, ImagesSubdir);
            List<FolderItem> items = new List<FolderItem>();
            var dirs = Directory.EnumerateDirectories(imageRoot);
            foreach(var dir in dirs)
            {
                items.Add(new FolderItem{
                    IsFolder = true,
                    Path = MakeWebDir(dir, rootPath),
                    Name = GetSubdirName(dir)
                });
            }

            var files = Directory.EnumerateFiles(imageRoot);
            foreach(var file in files)
            {
                items.Add(new FolderItem{
                    IsFolder = false,
                    Path = MakeWebDir(file, rootPath),
                    Name = Path.GetFileName(file)
                });
            }

            return new Folder{
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
        private static string TrimName(string name)
        {
            var result = name.Trim();
            result = name.Replace("\"", string.Empty);
            return result;
        }

        private string GetSubdirName(string fileSystemDir)
        {
            string[] elems = fileSystemDir.Split(new char[]{'\\'});
            return elems.Last();
        }

        private string MakeWebDir(string fileSystemDir, string rootPath)
        {
            var trimmed = fileSystemDir.Substring(rootPath.Length);
            var result = trimmed.Replace('\\', '/');
            return result;
        }

        private string MakeFileSystemPath(string subDir)
        {
            return subDir.Replace('/', '\\');
        }

        private string GetServerRootPath()
        {
            var context = (System.Web.HttpContextWrapper)this.Request.Properties["MS_HttpContext"];
            var variable = context.Request.ServerVariables["APPL_PHYSICAL_PATH"];
            return variable;
        }
    }
}