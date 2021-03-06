using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
using ZonyLrcTools.Cli.Infrastructure.Lyric;

namespace ZonyLrcTools.Tests.Infrastructure.Lyric
{
    public class NetEaseLyricDownloaderTests : TestBase
    {
        private readonly ILyricDownloader _lyricDownloader;

        public NetEaseLyricDownloaderTests()
        {
            _lyricDownloader = GetService<IEnumerable<ILyricDownloader>>()
                .FirstOrDefault(t => t.DownloaderName == InternalLyricDownloaderNames.NetEase);
        }

        [Fact]
        public async Task DownloadAsync_Test()
        {
            var lyric = await _lyricDownloader.DownloadAsync("Hollow", "Janet Leon");
            lyric.ShouldNotBeNull();
            lyric.IsPruneMusic.ShouldBe(false);
        }

        [Fact]
        public async Task DownloadAsync_Issue_75_Test()
        {
            var lyric = await _lyricDownloader.DownloadAsync("Daybreak", "samfree,初音ミク");
            lyric.ShouldNotBeNull();
            lyric.IsPruneMusic.ShouldBe(false);
            lyric.ToString().Contains("惑う心繋ぎ止める").ShouldBeTrue();
        }
    }
}