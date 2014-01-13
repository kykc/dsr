## Description

Simple disk usage reporter

## Dependencies

Works both on linux (mono >= 2.10.8.1) and windows (.net >= 4.0)

## Building

# GNU/Linux using xbuild

cd DSR_PROJECT_PATH
mono --runtime=v4.0 NuGet.exe restore
xbuild /p:configuration=Release dsr.sln

NOTE: you should have working NuGet for this to work

# Windows using msbuild

coming soon

## Usage

Usage: dsr [options] path

general options:
  --limit=LIMIT, -l LIMIT            number of lines per report (default: 10)
  --enable-pause, --disable-pause    press any key prompt
  --nodef                            disables default set of reports
  --top-files                        enables largest files report
  --top-dirs                         enables largest directories report
  --enable-totals, --disable-totals  toggles totals section (default: on)
  --raw-file-length                  output file/directory size in bytes
  --license                          shows license
  --help, -h, /?                     this help page
