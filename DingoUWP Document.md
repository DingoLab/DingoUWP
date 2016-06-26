## 前言

​	这个文档是Dingo项目的Win10客户端文档，包括Win10客户端的大体需求说明，客户端设计说明，客户端源码说明，客户端测试说明，客户端部署说明与客户端使用说明。

​	客户端DingoUWP分为DingoUWP-Cshap与DingoUWP-HTML5。DingoUWP-Cshap使用C#+XAML编写，DingoUWP-HTML5由HTML5+CSS+JavaScript编写。Dingo客户端源代码遵循BSD3协议开源，源码托管在[DingoLab](https://github.com/DingoLab)上。

​	DingoUWP的名称是由Dingo项目名称和客户端类型(Universal Windows Platform)简称组合而成的。

​	DingoUWP在开发过程中参考了IT周见智先生的开源项目[CNBlogs](https://github.com/MS-UAP/cnblogs-UAP)和[ZhiHuDaily.UWP](https://github.com/sherlockchou86/ZhiHuDaily.UWP)，在此对IT周见智先生表示由衷的感谢。

​	这个文档将依次介绍需求，设计，源码，测试，部署和使用说明。

### 目录

1. 大体需求说明
2. 设计说明
3. 源码说明
4. 测试说明
5. 部署说明
6. 使用说明

### 大体需求说明

​	DingoUWP是Dingo项目的核心部分之一，负责直接与用户交互，与Dingo后端配合完成整个Dingo项目的业务流程。

### 设计说明

​	DingoUWP是运行在Windows 10和Windows 10 Mobile上的UWP(Universal Windows Platform)应用程序。借助于微软的Win10统一平台战略，未来DingoUWP还有可能运行在游戏主机Xbox，增强现实设备HoloLens，巨屏触控Surface Hub以及其他安装了Windows 10的设备上。

​	DingoUWP分为DingoUWP-Cshap与DingoUWP-HTML5两个部分。

​	DingoUWP-Cshap使用C#+XAML开发，是专为Win10设计的，旨在为用户提供原生Win10UWP应用体验，Dingo用户在Windows 10平台上的所有操作都将通过DingoUWP进行，DingoUWP-Cshap是此次Dingo项目前端重点工程。

​	DingoUWP-HTML5由HTML5+CSS+JavaScript编写，旨在Dingo项目上线初期可以快速将DingoUWP移植到iOS平台和Android平台上，尽快覆盖到更可能多的用户，并在Dingo在iOS平台和Android平台上的专属客户端开发期间代替专属客户端的作用。借助HTML5优秀的兼容性和可移植性，iOS平台和Android平台开发者可以较为容易的将DingoUWP-HTML5移植到对应平台，DingoUWP-HTML5作为本次Dingo项目的前段试验性工程，将在DingoUWP-Cshap工程发布后开始进行。

​	DingoUWP开发中使用了MVVM框架和SuperWebSocket框架，测试阶段使用二进制文件分发，在完成基本功能测试后将考虑上架Win10应用商店，通过Win10应用商店进行分发。

### 源码说明

> 开发环境：Windows 10 10586，Microsoft Visual Studio 2015 Update1，Windows 10 SDK 10586。

1. DingoUWP_Csharp.Assetss

   此部分为DingoUWP的资源文件及其说明

2. DingoUWP_Csharp.Common

   此部分为DingoUWP的导航框架及其说明

3. DingoUWP_Csharp.Data

   此部分为DingoUWP的静态数据及其说明

   > DingoUWP_Csharp.Data.DataConvert
   >
   > 数据转换方法
   >
   > >DingoUWP_Csharp.Data.DataConvert.StreamToString(System.IO.Stream)
   > >
   > >Stream转String
   >
   > > DingoUWP_Csharp.Data.DataConvert.StringToStream(System.String)
   > >
   > > String 转 Stream
   >
   > > DingoUWP_Csharp.Data.DataConvert.BytesToImage(System.Byte[])
   > >
   > > Bytes转BitmapImage
   >
   > > DingoUWP_Csharp.Data.DataConvert.ImageToBytes(Windows.UI.Xaml.Media.ImageSource)
   > >
   > > 图片转bytes
   >
   > > DingoUWP_Csharp.Data.DataConvert.BytesToStream(System.Byte[])
   > >
   > > bytes转Stream
   >
   > > DingoUWP_Csharp.Data.DataConvert.StreamToBytes(System.IO.Stream)
   > >
   > > Stream转Bytes
   >
   > > DingoUWP_Csharp.Data.DataConvert.Encryption(System.String,System.String)
   > >
   > > 获取密码散列值
   >
   > > DingoUWP_Csharp.Data.DataConvert.SHA256Encrypt(System.String)
   > >
   > > 获取明文的SHA256散列值
   >
   > > DingoUWP_Csharp.Data.DataConvert.SHA512Encrypt(System.String)
   > >
   > > 获取明文的SHA512散列值
   >
   > > DingoUWP_Csharp.Data.DataConvert.Md5Encrypt(System.String)
   > >
   > > 获取明文的Md5散列值
   >
   > > DingoUWP_Csharp.Data.DataConvert.GetTimeStamp
   > >
   > > 获取时间戳

4. DingoUWP_Csharp.HTTP

   此部分为DingoUWP的HTTP服务及其说明

   > DingoUWP_Csharp.HTTP.apiURL
   >
   > Dingo后端提供的API
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_UserSignUp
   > >
   > > 用户注册
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_Authentication
   > >
   > > 用户身份认证
   >
   > >DingoUWP_Csharp.HTTP.apiURL.API_GetAuthenticationStatus
   > >
   > >查询身份认证状态
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_UserLogIn
   > >
   > > 用户登录
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_UserLogOut
   > >
   > > 用户登出
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_GetUserInfo
   > >
   > > 获取用户信息
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_GetUserHeadImage
   > >
   > > 获取用户头像
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_ChangeUserInfo
   > >
   > > 修改用户信息
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_ChangeUserPassword
   > >
   > > 修改用户密码
   >
   > >DingoUWP_Csharp.HTTP.apiURL.API_ChangeShippingAddress
   > >
   > >修改收货地址
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_GetShippingAddress
   > >
   > > 查询收货地址
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_PublishMission
   > >
   > > 任务发布（代收快递）
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_MissionChangeOrDelete
   > >
   > > 任务的修改或删除
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_GetMission
   > >
   > > 获取任务（代收快递）
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_NegotiatePrice
   > >
   > > 协商价格
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_GetMissionInfo
   > >
   > > 获取任务信息
   >
   > >DingoUWP_Csharp.HTTP.apiURL.API_PaymentAndConfirm
   > >
   > >支付和确认
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_ChangeOrAddCanInwardCollectionStatus
   > >
   > > 可代收状态的修改或添加
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_DeleteOrCompleteCanInwardCollectionStatus
   > >
   > > 可代收状态的删除或完成
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_GetCanInwardCollectionStatus
   > >
   > > 可代收状态查询
   >
   > > DingoUWP_Csharp.HTTP.apiURL.API_NotifyMessages
   > >
   > > 消息通告 ​
   > >
   > DingoUWP_Csharp.HTTP.BaseService
   > 
   > 基础HTTP访问服务
   > > DingoUWP_Csharp.HTTP.BaseService.HTTP_SendGetAsString(System.String)
   > >
   > > 向服务器发送get请求，返回服务器回复字符串数据 ​
   >
   > > DingoUWP_Csharp.HTTP.BaseService.HTTP_SendPostAsDictionary(System.String,System.String)
   > >
   > > 向服务器发送post请求,无附加的头信息，返回包含状态指示和服务器回复字符串数据的字典 ​
   >
   > > DingoUWP_Csharp.HTTP.BaseService.HTTP_SendPostAsDictionary(System.String,System.String,System.String[],System.String[])
   > >
   > > 向服务器发送post请求,存在附加的头信息，返回包含状态指示和服务器回复字符串数据的字典 ​
   >
   > > DingoUWP_Csharp.HTTP.BaseService.SendPostAsBytes(System.String,System.String)
   > >
   > > 向服务器发送post请求，返回服务器回复Bytes数据 ​
   >
   > > DingoUWP_Csharp.HTTP.BaseService.SendPostAsInputStream(System.String,System.String)
   > >
   > > 向服务器发送post请求，返回服务器回复InputStream数据 ​
   >
   > > DingoUWP_Csharp.HTTP.DingoDataException
   > >
   > > 当Dingo后端返回了无法使用的信息时抛出的异常 ​
   >

5. DingoUWP_Csharp.Models

   此部分为DingoUWP的数据模型及其说明

6. DingoUWP_Csharp.Page

   此部分为DingoUWP的页面及其说明


### 测试说明

> 测试环境：Windows 10 10240及其以上，Windows 10 Mobile 10240及其以上。

​	测试过程中客户端将以二进制文件包的形式发放。文件包包包含x86，x64，ARM三个平台的二进制程序文件，PowerShell安装脚本和客户端开发人员证书。

​	测试人员应按如下步骤部署客户端：

1. 进入“设置-更新和安全”，选中“针对开发人员”，选中“开发者模式”。

2. 在文件包中的PowerShall安装脚本上鼠标右击，选择“使用PowerShall运行”，首次运行有可能会提示“执行策略更改”，按默认输入“N”后回车即可。

3. 再重新在文件包中的PowerShall安装脚本上鼠标右击，选择“使用PowerShall运行”，弹出Windows Power Shell窗口，提示“安装签名证书”，按Enter键继续。

4. 弹出一个新的Windows Power Shell窗口，提示“正在安装证书”，同时询问“是否确实要继续？”。输入 Y 后回车。

5. 当提示“成功安装了应用程序”时，客户端已经部署到了计算技上。

   ​



### 部署说明
> 部署环境：Windows 10 10240及其以上，Windows 10 Mobile 10240及其以上。



### 使用说明
> 使用环境：Windows 10 10240及其以上，Windows 10 Mobile 10240及其以上。支持x64，x86，ARM处理器。



