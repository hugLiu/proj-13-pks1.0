﻿/**
 * 
 * 类描述: 基础开发平台前台全局变量定义及常量配置，用于定义平台前台页面的全局变量及常量配置
 * 		  分为“基础开发平台配置区域”和“业务系统配置区域”。“基础开发平台配置区域”定义的变量
 *        和常量不允许删除。基于基础开发平台开发的业务系统，可以根据实际情况修改平台的常量
 *        配置值，也可以在“业务系统配置区域”添加业务系统需要的变量定义和常量配置。
 *
 * @author yanght
 * @version 1.0
 * 创建时间： 2012-10-30 上午8:46:07
 *********************************更新记录******************************
 * 版本：  1.0       修改日期：         修改人：
 * 修改内容： 
 **********************************************************************
 */

/** *********************基础开发平台配置区域--开始*********************** */

var toShow2d = false;// 是否显示地图，true为显示，false为不显示

var toShow3d = true;// 是否显示三维，true为显示，false为不显示

var defaultPageLayout = 1;// 系统默认布局，1为上下布局，2为左右布局

// 默认打开的tab标签数组，格式为[{id:'1010',title:'待办工作',url:'../../platform/workflow/myundotask.htm',closeable:'false'},{}]
var defaultOpenTabArray = [];

var hasUserPreferenceConfig = false; // 是否开启用户个性化设置功能，即系统里面是否允许用户进行个性化设置，true为开启，false为不开启

// 允许打开的tab页签个数
var TAB_COUNT = 5;

// 地图对象
var mapViewer = null;

// 二三维是否联动
var isLinkage = false;

// 二三维联动是否正在进行
var inLinkage = false;

// 定位二维级数与三维相机高度的关联Json
var mapLevelToZ = {
	1 : 320000,
	2 : 140000,
	3 : 50000,
	4 : 25000,
	5 : 12500,
	6 : 6000,
	7 : 3000,
	8 : 1500,
	9 : 750,
	10 : 350,
	11 : 200
};

// 用户图层权限集合
var verifiedLayer = "";

// 客户端语言，登录和语言切换时要更新该变量值
var language = getParamter('i18n');	//当前语言
if(language==""){
	language = "zh_CN";
}

// 应用系统编号
var APP_NUMBER = 1;

//登录用户的userBo对象
var loginUser;

/** *********************基础开发平台配置区域--结束*********************** */
/* ====================================================================== */
/* ====================================================================== */
/* ====================================================================== */
/** *********************业务系统配置区域-开始**************************** */

/** *********************业务系统配置区域-结束**************************** */

