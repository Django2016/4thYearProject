using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace VidEye.Models
{
    public class VidoeUploadManager
    {
        public string StreamingUri { get; set; }
        public string ThumbnailUri { get; set; }
        private  CloudMediaContext _context = null;
        private  readonly string _mediaServicesAccountName =
           ConfigurationManager.AppSettings["MediaServicesAccountName"];
        private  readonly string _mediaServicesAccountKey =
            ConfigurationManager.AppSettings["MediaServicesAccountKey"];

        private  readonly string _mediaFiles =
                Path.GetFullPath(@"../..\Media");

        private readonly string _presetFiles =
            Path.GetFullPath(@"../..\Presets");

        private  MediaServicesCredentials _cachedCredentials = null;

        public VidoeUploadManager()
        {
            // Create and cache the Media Services credentials in a static class variable.
            _cachedCredentials = new MediaServicesCredentials(
                            _mediaServicesAccountName,
                            _mediaServicesAccountKey);
            // Used the chached credentials to create CloudMediaContext.
            _context = new CloudMediaContext(_cachedCredentials);
        }
        private IAsset GetAsset(AssetType type, IAsset inputAsset = null)
        {
            IAsset result;
            switch (type)
            {
                case AssetType.AudioOnly:
                    result = EncodeToAudioOnly(inputAsset, AssetCreationOptions.None); 
                    break;
                case AssetType.EncodedMp4:
                    result = EncodeToAdaptiveBitrateMP4s(inputAsset, AssetCreationOptions.None); ;
                    break;
                case AssetType.File:
                    result = UploadFile(Path.Combine(_mediaFiles, @"BigBuckBunny.mp4"), AssetCreationOptions.None);
                    break;
                case AssetType.Thumbnail:                                     
                default:
                    result = GenerateThumbnail(inputAsset, AssetCreationOptions.None);
                    break;
            }
            return result;
        }

        private IAsset EncodeToAdaptiveBitrateMP4s(IAsset asset, AssetCreationOptions options)
        {
            // Prepare a job with a single task to transcode the specified asset
            // into a multi-bitrate asset.
            IJob job = _context.Jobs.CreateWithSingleTask(
                "Media Encoder Standard",
                "H264 Multiple Bitrate 720p",
                asset,
                "Adaptive Bitrate MP4",
                options);

            Console.WriteLine("Submitting transcoding job...");


            // Submit the job and wait until it is completed.
            job.Submit();

            job = job.StartExecutionProgressTask(
                j =>
                {
                    Console.WriteLine("Job state: {0}", j.State);
                    Console.WriteLine("Job progress: {0:0.##}%", j.GetOverallProgress());
                },
                CancellationToken.None).Result;

            Console.WriteLine("Transcoding job finished.");

            IAsset outputAsset = job.OutputMediaAssets[0];

            return outputAsset;
        }

        private IAsset UploadFile(string fileName, AssetCreationOptions options)
        {
            IAsset inputAsset = _context.Assets.CreateFromFile(
                fileName,
                options,
                (af, p) =>
                {                    
                });           
            return inputAsset;
        }

        private IAsset GenerateThumbnail(IAsset asset, AssetCreationOptions options)
        {
            // Load the XML (or JSON) from the local file.
            string configuration = File.ReadAllText(Path.Combine(_presetFiles, @"ThumbnailPreset_JSON.json"));
            IJob job = _context.Jobs.CreateWithSingleTask(
                "Media Encoder Standard",
                configuration,
                asset,
                "Thumbnail",
                options);

            Console.WriteLine("Submitting transcoding job...");
            // Submit the job and wait until it is completed.
            job.Submit();
            job = job.StartExecutionProgressTask(
                j =>
                {
                    Console.WriteLine("Job state: {0}", j.State);
                    Console.WriteLine("Job progress: {0:0.##}%", j.GetOverallProgress());
                },
                CancellationToken.None).Result;
            Console.WriteLine("Transcoding job finished.");
            IAsset outputAsset = job.OutputMediaAssets[0];
            return outputAsset;
        }

        private IAsset EncodeToAudioOnly(IAsset asset, AssetCreationOptions options)
        {
            // Load the XML (or JSON) from the local file.
            string configuration = File.ReadAllText(Path.Combine(_presetFiles, @"AudioOnlyPreset_JSON.json"));

            IJob job = _context.Jobs.CreateWithSingleTask(
                "Media Encoder Standard",
                configuration,
                asset,
                "Audio only",
                options);

            Console.WriteLine("Submitting transcoding job...");


            // Submit the job and wait until it is completed.
            job.Submit();

            job = job.StartExecutionProgressTask(
                j =>
                {
                    Console.WriteLine("Job state: {0}", j.State);
                    Console.WriteLine("Job progress: {0:0.##}%", j.GetOverallProgress());
                },
                CancellationToken.None).Result;

            Console.WriteLine("Transcoding job finished.");

            IAsset outputAsset = job.OutputMediaAssets[0];

            return outputAsset;
        }

        public void PublishAsset(IAsset asset, bool onDemaindURL = true, string fileExt = "")
        {
            // Publish the output asset by creating an Origin locator for adaptive streaming,
            // and a SAS locator for progressive download.

            if (onDemaindURL)
            {
                _context.Locators.Create(
                    LocatorType.OnDemandOrigin,
                    asset,
                    AccessPermissions.Read,
                    TimeSpan.FromDays(30));

                // Get the Smooth Streaming, HLS and MPEG-DASH URLs for adaptive streaming,
                // and the Progressive Download URL.
                Uri smoothStreamingUri = asset.GetSmoothStreamingUri();
                Uri hlsUri = asset.GetHlsUri();
                Uri mpegDashUri = asset.GetMpegDashUri();

                // Display  the streaming URLs.
                Console.WriteLine("Use the following URLs for adaptive streaming: ");
                Console.WriteLine(smoothStreamingUri);
                Console.WriteLine(hlsUri);
                Console.WriteLine(mpegDashUri);
                Console.WriteLine();
            }
            else
            {
                _context.Locators.Create(
                    LocatorType.Sas,
                    asset,
                    AccessPermissions.Read,
                    TimeSpan.FromDays(30));

                IEnumerable<IAssetFile> assetFiles = asset
                    .AssetFiles
                    .ToList()
                    .Where(af => af.Name.EndsWith(fileExt, StringComparison.OrdinalIgnoreCase));


                // Get the URls for progressive download for each specified file that was generated as a result
                // of encoding.

                List<Uri> sasUris = assetFiles.Select(af => af.GetSasUri()).ToList();

                // Display the URLs for progressive download.
                Console.WriteLine("Use the following URLs for progressive download.");
                sasUris.ForEach(uri => Console.WriteLine(uri + "\n"));
                Console.WriteLine();
            }
        }
    }
}