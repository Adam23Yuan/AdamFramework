using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Adam.WebApi.Extensions
{
    public static class ResponseFileExtension
    {
        public static IActionResult ResponseFile(this HttpContext httpContext, string fileFullName)
        {
            string fileName = Path.GetFileName(fileFullName);
            //头部保存 文件名
            httpContext.Response.Headers.Append("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode($"{fileName}"));
            //文件类型
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            if (!System.IO.File.Exists(fileFullName))
            {
                return new EmptyResult();
            }
            string fileExtions = Path.GetExtension(fileFullName);
            //获取文件类型
            string contentType = "application/octet-stream";
            string mime = string.Empty;
            bool flag = provider.Mappings.TryGetValue(fileExtions, out mime);
            if (flag)
            {
                contentType = mime;
            }
            FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
            //读取文件流
            return new FileStreamResult(fs, contentType);
        }

        /// <summary>
        /// <para> seealso  https://www.cnblogs.com/yangxu-pro/p/8722498.html</para>
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="fileFullName"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static IActionResult ResponseFileStream(this HttpContext httpContext, string fileFullName, int times)
        {
            string fileName = Path.GetFileName(fileFullName);
            //头部保存 文件名
            httpContext.Response.Headers.Append("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode($"{fileName}"));
            //文件类型
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            //if (!System.IO.File.Exists(fileFullName))
            //{
            //    return new EmptyResult();
            //}
            string fileExtions = Path.GetExtension(fileFullName);
            //获取文件类型
            string contentType = "application/octet-stream";
            string mime = string.Empty;
            bool flag = provider.Mappings.TryGetValue(fileExtions, out mime);
            if (flag)
            {
                contentType = mime;
            }
            //FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);

            //return new FileStreamResult(fs, contentType);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BufferedStream bufferedStream = new BufferedStream(memoryStream);
                CryptoStream cryptoStream;//
                GZipStream gZipStream = new GZipStream(memoryStream, CompressionLevel.Fastest);
                //customer content
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(contentType);
                stringBuilder.Append($"download file content");
                #region filecontent
                //            stringBuilder.Append(@"
                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devmasterAvatarWebApi)
                //$ git checkout master
                //Switched to branch 'master'
                //Your branch is behind 'origin/master' by 3 commits, and can be fast-forwarded.
                //  (use 'git pull' to update your local branch)

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (master)
                //$ git pull origin master:master
                //From https://msasg.visualstudio.com/DefaultCollection/Bing%20Multimedia%20Tools/_git/Bing-MM-Tools
                //   4b69f979f..7e13f8e9e  master     -> master
                //warning: fetch updated the current branch head.
                //fast-forwarding your working tree from
                //commit 4b69f979f60d25028d928c41aed1a742de6b6dad.
                //Already up to date.

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (master)
                //$ git checkout devmasterAvatarWebApi
                //Switched to branch 'devmasterAvatarWebApi'
                //Your branch is up to date with 'origin/devmasterAvatarWebApi'.

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devmasterAvatarWebApi)
                //$ git merge master
                //Merge made by the 'ort' strategy.
                // .../UHRSClasses/UHRSJudgmentFinished.cs            |     21 +-
                // .../Pbxml/BingCollectionsSearchAnswerParser.cs     |     95 +
                // .../Avatar/Libs/HRSCommon/DataItemConstants.cs     |      3 +
                // .../BingCollectionsSearchAnswerParserTests.cs      |     33 +
                // .../BingCollectionsSearchAnswerParserTestData1.xml | 127902 ++++++++++++++++++
                // .../DataParserLibTests/DataParserLibTests.csproj   |      4 +
                // .../JobCreationAutomation/ScrapeJobCreator.cs      |      2 +-
                // .../UploadScrapeResultsJobCreator.cs               |      5 +-
                // 8 files changed, 128056 insertions(+), 9 deletions(-)
                // create mode 100644 private/Pipelines/Avatar/Libs/DataParserLib/Bing/Pbxml/BingCollectionsSearchAnswerParser.cs
                // create mode 100644 private/Pipelines/Avatar/Tests/Unit/DataParserLibTests/BingCollectionsSearchAnswerParserTests.cs
                // create mode 100644 private/Pipelines/Avatar/Tests/Unit/DataParserLibTests/DataParserLibFiles/BingCollectionsSearchAnswerParserTestsFiles/BingCollectionsSearchAnswerParserTestData1.xml

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devmasterAvatarWebApi)
                //$ git push origin devmasterAvatarWebApi
                //Enumerating objects: 16, done.
                //Counting objects: 100% (16/16), done.
                //Delta compression using up to 8 threads
                //Compressing objects: 100% (6/6), done.
                //Writing objects: 100% (6/6), 622 bytes | 622.00 KiB/s, done.
                //Total 6 (delta 5), reused 0 (delta 0), pack-reused 0
                //remote: Analyzing objects... (6/6) (222 ms)
                //remote: Checking for credentials and other secrets...  done (311 ms)
                //remote: Storing packfile... done (51 ms)
                //remote: Storing index... done (43 ms)
                //To https://msasg.visualstudio.com/DefaultCollection/Bing%20Multimedia%20Tools/_git/Bing-MM-Tools
                //   497feeb63..e61809a77  devmasterAvatarWebApi -> devmasterAvatarWebApi

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devmasterAvatarWebApi)
                //$ git checkout devluois
                //Switched to branch 'devluois'
                //Your branch is behind 'origin/devluois' by 3 commits, and can be fast-forwarded.
                //  (use 'git pull' to update your local branch)

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devluois)
                //$ git pull origin devluois
                //From https://msasg.visualstudio.com/DefaultCollection/Bing%20Multimedia%20Tools/_git/Bing-MM-Tools
                // * branch                devluois   -> FETCH_HEAD
                //Updating 08f9175c1..f24d5a28f
                //Fast-forward
                // .../Avatar.Core/Constants/ConstantQuantity.cs      |   2 +
                // .../Dtos/Job/InputClonJobAsJobRecurrenceDto.cs     |  16 +-
                // .../Avatar.Services/Dtos/Job/InputJobInfoDto.cs    |  16 +
                // .../ScrapeJobDetails/OutputScrapeJobDetailDto.cs   |   4 +
                // .../Avatar.Services/Modules/AvatarJob/IJob.cs      |   9 +
                // .../Modules/AvatarJob/JobService.cs                | 381 +++++++++++++--------
                // .../IScrapeJobRecurrenceEngine.cs                  |  17 +
                // .../ScrapeJobRecurrenceEngineeService.cs           |  89 +++++
                // .../Controllers/ConstantController.cs              |  18 +
                // .../Avatar.WebApi/Controllers/JobController.cs     |  35 +-
                // .../JobStateTransitionActionsController.cs         |   8 +-
                // .../ScrapeJobRecurrenceEngineeController.cs        |  42 +++
                // .../Extenstions/MappingExtenstions.cs              |  11 +-
                // 13 files changed, 479 insertions(+), 169 deletions(-)
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Dtos/Job/InputJobInfoDto.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Modules/ScrapeJobRecurrenceEngines/IScrapeJobRecurrenceEngine.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Modules/ScrapeJobRecurrenceEngines/ScrapeJobRecurrenceEngineeService.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.WebApi/Controllers/ScrapeJobRecurrenceEngineeController.cs

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devluois)
                //$ git merge devmasterAvatarWebApi
                //Merge made by the 'ort' strategy.
                // .../JobReccurence/InputEditJobStateTransition.cs   |     15 +
                // .../Dtos/JobReccurence/InputTableUpdate.cs         |     21 +
                // .../JobReccurence/JobStateTransitionTableDTO.cs    |     33 +
                // .../Dtos/JobReccurence/OutputEngineTableData.cs    |     32 +
                // .../Dtos/JobReccurence/ScrapeEngineDto.cs          |     32 +
                // .../JobReccurence/ScrapeJobRecurrenceEngineDto.cs  |     31 +
                // .../Modules/RecurringJob/IJobRecurrence.cs         |     44 +
                // .../Modules/RecurringJob/JobRecurrenceService.cs   |    195 +-
                // .../PartialServices/JobRecurrenceDetailService.cs  |    133 +-
                // .../Controllers/JobRecurrenceController.cs         |     31 +-
                // .../UHRSClasses/UHRSJudgmentFinished.cs            |     21 +-
                // .../Pbxml/BingCollectionsSearchAnswerParser.cs     |     95 +
                // .../Avatar/Libs/HRSCommon/DataItemConstants.cs     |      3 +
                // private/Pipelines/Avatar/SM2/SM2.csproj            |      2 +
                // .../BingCollectionsSearchAnswerParserTests.cs      |     33 +
                // .../BingCollectionsSearchAnswerParserTestData1.xml | 127902 ++++++++++++++++++
                // .../DataParserLibTests/DataParserLibTests.csproj   |      4 +
                // .../JobCreationAutomation/ScrapeJobCreator.cs      |      2 +-
                // .../UploadScrapeResultsJobCreator.cs               |      5 +-
                // 19 files changed, 128615 insertions(+), 19 deletions(-)
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Dtos/JobReccurence/InputEditJobStateTransition.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Dtos/JobReccurence/InputTableUpdate.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Dtos/JobReccurence/JobStateTransitionTableDTO.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Dtos/JobReccurence/OutputEngineTableData.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Dtos/JobReccurence/ScrapeEngineDto.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Dtos/JobReccurence/ScrapeJobRecurrenceEngineDto.cs
                // create mode 100644 private/Pipelines/Avatar/Libs/DataParserLib/Bing/Pbxml/BingCollectionsSearchAnswerParser.cs
                // create mode 100644 private/Pipelines/Avatar/Tests/Unit/DataParserLibTests/BingCollectionsSearchAnswerParserTests.cs
                // create mode 100644 private/Pipelines/Avatar/Tests/Unit/DataParserLibTests/DataParserLibFiles/BingCollectionsSearchAnswerParserTestsFiles/BingCollectionsSearchAnswerParserTestData1.xml

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devluois)
                //$ git push origin devluois
                //Enumerating objects: 31, done.
                //Counting objects: 100% (31/31), done.
                //Delta compression using up to 8 threads
                //Compressing objects: 100% (11/11), done.
                //Writing objects: 100% (11/11), 980 bytes | 980.00 KiB/s, done.
                //Total 11 (delta 9), reused 0 (delta 0), pack-reused 0
                //remote: Analyzing objects... (11/11) (226 ms)
                //remote: Checking for credentials and other secrets...  done (77 ms)
                //remote: Storing packfile... done (67 ms)
                //remote: Storing index... done (42 ms)
                //To https://msasg.visualstudio.com/DefaultCollection/Bing%20Multimedia%20Tools/_git/Bing-MM-Tools
                //   f24d5a28f..0e5304a08  devluois -> devluois

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devluois)
                //$ git checkout devmasterAvatarWebApi
                //Switched to branch 'devmasterAvatarWebApi'
                //Your branch is up to date with 'origin/devmasterAvatarWebApi'.

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devmasterAvatarWebApi)
                //$ git merge devluois
                //Updating e61809a77..0e5304a08
                //Fast-forward
                // .../Avatar.Core/Constants/ConstantQuantity.cs      |   2 +
                // .../Dtos/Job/InputClonJobAsJobRecurrenceDto.cs     |  16 +-
                // .../Avatar.Services/Dtos/Job/InputJobInfoDto.cs    |  16 +
                // .../ScrapeJobDetails/OutputScrapeJobDetailDto.cs   |   4 +
                // .../Avatar.Services/Modules/AvatarJob/IJob.cs      |   9 +
                // .../Modules/AvatarJob/JobService.cs                | 381 +++++++++++++--------
                // .../IScrapeJobRecurrenceEngine.cs                  |  17 +
                // .../ScrapeJobRecurrenceEngineeService.cs           |  89 +++++
                // .../Controllers/ConstantController.cs              |  18 +
                // .../Avatar.WebApi/Controllers/JobController.cs     |  35 +-
                // .../JobStateTransitionActionsController.cs         |   8 +-
                // .../ScrapeJobRecurrenceEngineeController.cs        |  42 +++
                // .../Extenstions/MappingExtenstions.cs              |  11 +-
                // 13 files changed, 479 insertions(+), 169 deletions(-)
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Dtos/Job/InputJobInfoDto.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Modules/ScrapeJobRecurrenceEngines/IScrapeJobRecurrenceEngine.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.Services/Modules/ScrapeJobRecurrenceEngines/ScrapeJobRecurrenceEngineeService.cs
                // create mode 100644 private/Pipelines/Avatar/Avatar/Avatar.WebApi/Controllers/ScrapeJobRecurrenceEngineeController.cs

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devmasterAvatarWebApi)
                //$ git add .

                //FAREAST+v-siyongyuan@DESKTOP-TM6E4QI MINGW64 /d/Work/Bing-MM-Tools (devmasterAvatarWebApi)
                //$ git push");

                #endregion
                //file content
                string alllines = System.IO.File.ReadAllText(fileFullName);
                /* MemoryStream */
                //memoryStream.WriteAsync(System.IO.File.ReadAllBytes(fileFullName));
                //memoryStream.Write(Encoding.Default.GetBytes(contentType));
                //memoryStream.Write(Encoding.Default.GetBytes("download file content"));
                /* TextWriter=>StreamWriter */
                TextWriter writer = new StreamWriter(memoryStream, Encoding.UTF8, 1);

                //Stream 适合器类
                //TextWriter stringWriter = new StringWriter();
                //TextReader textReader = File.OpenText(fileFullName);
                //StringWriter sw = new StringWriter();
                //StringReader stringReader = new StringReader(alllines);
                //XmlReader xmlReader = XmlReader.Create(stringReader);
                //XmlWriter xmlWriter = XmlWriter.Create(memoryStream);

                BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                for (int i = 0; i < times; i++)
                {
                    writer.Write(stringBuilder.ToString());
                    //binaryWriter.Write(stringBuilder.ToString());
                }
                // 使用 StreamWriter 适合器时，需要调用 Flush  因为当写入的内容不够1024B时，不会写入内存流对象中。
                writer.Flush();
                // 使用 BinaryWriter 适合器时 可不调用 Flush
                //binaryWriter.Flush();

                //writer.Write(alllines);
                //streamWriter.WriteAsync(System.IO.File.ReadAllText(fileFullName));
                //读取文件流
                //return new FileStreamResult(memoryStream, contentType);
#pragma warning disable CS8604 // Possible null reference argument.
                return new FileContentResult(memoryStream.ToArray(), contentType);
#pragma warning restore CS8604 // Possible null reference argument.
            }
        }
    }
}
