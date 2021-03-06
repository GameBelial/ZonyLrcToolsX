﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using ZonyLrcTools.Cli.Infrastructure.Album;

namespace ZonyLrcTools.Tests.Infrastructure.Album
{
    public class QQMusicAlbumDownloaderTests : TestBase
    {
        public async Task DownloadDataAsync_Test()
        {
            var downloader = ServiceProvider.GetRequiredService<IEnumerable<IAlbumDownloader>>()
                .FirstOrDefault(x => x.DownloaderName == InternalAlbumDownloaderNames.QQ);

            downloader.ShouldNotBeNull();
            var albumBytes = await downloader.DownloadAsync("东方红", null);
            albumBytes.Length.ShouldBeGreaterThan(0);

            // 显示具体的图像。
            var tempAlbumPath = Path.Combine(Directory.GetCurrentDirectory(), "tempAlbum.png");
            File.Delete(tempAlbumPath);

            await using var file = File.Create(tempAlbumPath);
            await using var ws = new BinaryWriter(file);
            ws.Write(albumBytes);
            ws.Flush();
        }
    }
}