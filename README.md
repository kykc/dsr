## Description

Simple disk usage reporter (just an education task for myself, trying to write something cross-platform on .NET)

## Building

Can be built using `dotnet` cli or Visual Studio

## Usage
```
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
```