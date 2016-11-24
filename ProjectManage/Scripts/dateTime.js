

// 获取当前日期
function dateTimeNow(format) {
    var strDateTime;
    var dateTimeNow = new Date();
//    var strYear = dateTimeNow.getFullYear();
//    var strMonth = dateTimeNow.getMonth() + 1;
//    if (strMonth < 10)
//        strMonth = "0" + strMonth;
//    var strDay = dateTimeNow.getDate();
//    if (strDay < 10)
//        strDay = "0" + strDay;
//    var strHours = dateTimeNow.getHours();
//    if (strHours < 10)
//        strHours = "0" + strHours;
//    var strMinutes = dateTimeNow.getMinutes();
//    if (strMinutes < 10)
//        strMinutes = "0" + strMinutes;
//    var strSeconds = dateTimeNow.getSeconds();
//    if (strSeconds < 10)
//        strSeconds = "0" + strSeconds;
//    
//    if(format.toLowerCase()== 'yyyy-mm-dd hh:mm:ss'){
//        strDateTime = strYear + "-" + strMonth + "-" + strDay + " " + strHours + ":" + strMinutes + ":" + strSeconds;
//    }
//    else if(format.toLowerCase()== 'yyyy-mm-dd'){
//        strDateTime = strYear + "-" + strMonth + "-" + strDay ;
//    }
    strDateTime = dateToString(dateTimeNow,format);
    return strDateTime;
}

//将日期格式转换成字符串
function dateToString(date, format) {
    var strDateTime;
    var strYear = date.getFullYear();
    var strMonth = date.getMonth() + 1;
    if (strMonth < 10)
        strMonth = "0" + strMonth;
    var strDay = date.getDate();
    if (strDay < 10)
        strDay = "0" + strDay;
    var strHours = date.getHours();
    if (strHours < 10)
        strHours = "0" + strHours;
    var strMinutes = date.getMinutes();
    if (strMinutes < 10)
        strMinutes = "0" + strMinutes;
    var strSeconds = date.getSeconds();
    if (strSeconds < 10)
        strSeconds = "0" + strSeconds;

    if (format.toLowerCase() == 'yyyy-mm-dd hh:mm:ss') {
        strDateTime = strYear + "-" + strMonth + "-" + strDay + " " + strHours + ":" + strMinutes + ":" + strSeconds;
    }
    else if (format.toLowerCase() == 'yyyy-mm-dd hh:mm') {
        strDateTime = strYear + "-" + strMonth + "-" + strDay + " " + strHours + ":" + strMinutes;
    }
    else if (format.toLowerCase() == 'yyyy-mm-dd') {
        strDateTime = strYear + "-" + strMonth + "-" + strDay;
    }
    else if (format.toLowerCase() == 'yyyy-mm') {
        strDateTime = strYear + "-" + strMonth;
    }
    else if (format.toLowerCase() == 'yyyymmdd') {
        strDateTime = strYear + "" + strMonth + "" + strDay;
    }
    else if (format.toLowerCase() == 'yyyymmddhhmm') {
        strDateTime = strYear + "" + strMonth + "" + strDay + "" + strHours + "" + strMinutes;
    } else if (format.toLowerCase() == 'yyyy年mm月dd日') {
        strMonth = strMonth.toString();
        strDay = strDay.toString();
        if (strMonth.indexOf('0') == 0) {
            strMonth = strMonth.substring(1, strMonth.length);
        }
        if (strDay.indexOf('0') == 0) {
            strDay = strDay.substring(1, strDay.length);
        }
        strDateTime = strYear + "年" + strMonth + "月" + strDay + "日";
    } else if (format.toLowerCase() == 'yyyy年mm月dd日 hh时mm分') {
        strMonth = strMonth.toString();
        strDay = strDay.toString();
        strHours = strHours.toString();
        strMinutes = strMinutes.toString();
        if (strMonth.indexOf('0') == 0) {
            strMonth = strMonth.substring(1, strMonth.length);
        }
        if (strDay.indexOf('0') == 0) {
            strDay = strDay.substring(1, strDay.length);
        }
        if (strHours.indexOf('0') == 0) {
            strHours = strHours.substring(1, strHours.length);
        }
        if (strMinutes.indexOf('0') == 0) {
            strMinutes = strMinutes.substring(1, strMinutes.length);
        }
        strDateTime = strYear + "年" + strMonth + "月" + strDay + "日 " + strHours + "时" + strMinutes + "分";
    }
    return strDateTime;
}
