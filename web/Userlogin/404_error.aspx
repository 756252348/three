<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404_error.aspx.cs" Inherits="Userlogin_404_error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <title>未知错误</title>
    <style>
        body{
            background-color:#fff;
        }
        .main {
            width: 79.5%;
            height: auto;
            overflow: hidden;
            margin-top: 80px;
            margin-left: auto;
            margin-right: auto;
        }

        .top {
            position: relative;
            width: 59%;
            height: auto;
            margin-left: auto;
            margin-right: auto;
        }

        .pic1 {
            width: 100%;
            height: auto;
        }
        .div_img{
            position: absolute;
            top: 26%;
            right: 13%;
            width: 10%;
            height: auto;
            animation: mymove 3s infinite;
            -moz-animation: mymove 3s infinite; /* Firefox */
            -webkit-animation: mymove 3s infinite; /* Safari and Chrome */
            -o-animation: mymove 3s infinite; /* Opera */
        }
        .pic2 {
            width:100%;
            height:auto;
        }

        @keyframes mymove {
            from {
                top: 26%;
            }

            to {
                top: 58%;
            }
        }

        @-moz-keyframes mymove /* Firefox */
        {
           from {
                top: 26%;
            }

            to {
                top: 58%;
            }
        }

        @-webkit-keyframes mymove        /* Safari and Chrome */
        {
            from {
                top: 26%;
            }

            to {
                top: 58%;
            }
        }

        @-o-keyframes mymove /* Opera */
        {
           from {
                top: 26%;
            }

            to {
                top: 58%;
            }
        }

        .content {
            width: 100%;
            text-align: center;
            color: #808080;
        }

        .bottom {
            overflow: hidden;
            width: 100%;
            padding-top: 24px;
        }

            .bottom > input {
                float: left;
                border: 1px solid #01b4f6;
                padding: 4px 8px;
                font-size: 14px;
                color: #01b4f6;
                width: 33%;
                   -webkit-appearance:none;
            }

                .bottom > input:last-child {
                    float: right;
                }
    </style>
</head>

<body>
    <div class="main">
        <div class="top">
            <img class="pic1" src="../images/hhf_16.png" />
            <div class="div_img">
                <img class="pic2" src="../images/hhf_17.png" />
                </div>
        </div>
        <div class="content">
            <h2>404</h2>
            <p>亲！不好意思哦，上级出问题了哦！</p>
        </div>
        <div class="bottom">
            <input type="button" value="返回首页" onclick="window.location.href = 'userInfo.aspx'" />
            <input type="button" value="刷新试试" onclick="window.history.go(-1)" />
        </div>
    </div>
</body>
</html>

