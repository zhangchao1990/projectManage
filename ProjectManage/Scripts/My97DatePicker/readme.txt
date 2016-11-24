使用方法：
1、在页面的</body>的前面或后面一行添加引用My97DatePicker文件夹中的WdatePicker.js文件；
2、在要绑定的文本框上添加代码：onClick="WdatePicker({readOnly:true,dateFmt:'yyyy-MM-dd HH:mm:ss'})"；
3、如果不需要文本框只读，请将上面的代码中的readonly后面的true改成false即可；
4、此显示的日期格式如：2008-08-08 20:00:00 ，需要指定格式，可修改'yyyy-MM-dd HH:mm:ss'；
5、若需要时间限制，可在步骤2的代码中("ss'的后面")加代码：minDate:'2000-1-1',maxDate:'%y-%M-%d'，(%y-%M-%d'表示当前年月日);