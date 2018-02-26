# Steam Screenshot Importer

导入非Steam截图至Steam中

![预览](https://user-images.githubusercontent.com/12966814/36627620-3c880ea4-1980-11e8-8547-bfd8db32b519.png)

## 特性

- 自动检索Steam目录并获取用户和游戏信息

- 支持批量图像导入, 支持拖拽

## 使用

- 下载[SteamScreenshotImporter](https://github.com/Zaeworks/SteamScreenshotImporter/releases)

- 打开程序, 自动扫描Steam信息, 如果注册表中没有Steam注册信息请手动选择Steam根目录

- 选择要导入至的用户和游戏

- 将要导入的图像拖拽至程序中

- 导入!

## 依赖

- 用于解析Valve Data Format文件`.vdf`的[Gameloop.Vdf](https://github.com/shravan2x/Gameloop.Vdf)

## 更新

### v1.1

- 修复截图无法上传的问题
`Steam在screenshots.vdf中自动添入的截图信息导致非Steam无法上传, 故改由本程序在导入截图时添入截图记录`

- 修改缩略图尺寸为固定宽200
