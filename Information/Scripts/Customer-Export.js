
$(function () {
    if (typeof (jQuery) === 'undefined') { alert('jQuery Library NotFound'); return; }

    var HasData = 'False';
    var className;
   

    $("#ButtonExport").click(function () {
        className = $(this).data('classname');
        $("#ExportDataDialog").modal("show");
        
    });

    $("#SelectAllColumns").unbind('click').click(function () {
        $("input:checkbox[name=CheckBox_ExportColumns]").prop("checked", "checked");
    });

    $("#UnselectAllColumns").unbind('click').click(function () {
        $("input:checkbox[name=CheckBox_ExportColumns]").removeAttr("checked");
    });

    //匯出資料
    $('#ButtonExecuteExport').click(function () {
        
        var selectedColumns = $('input:checkbox[name=CheckBox_ExportColumns]:checked').map(function () {
            return $(this).val();
        }).get().join(',');
        
        if (selectedColumns.length == 0) {
            alert("必須選取資料欄位");
            return false;
        }
        ;
        var url = $(this).data('url');
        /**/
        //ExportData(selectedColumns, className);
        
        
        window.location.href = "/Excel/ExportFile?selectedColumns=" + selectedColumns + "&className=" + className;
        $("#ExportDataDialog").modal('hide');
        
        
       
       
    });
});

function ExportData(selectedColumns, className) {
    //無作用ing
    $.ajax({
        type: 'GET',
        url: Router.action("Excel", "JsonTest"),
        datatype: 'json',//打dataType會錯誤
        contentType: "application/json",
        //data: JSON.stringify({ selectedColumns: selectedColumns}),  //用data傳要stringify
        success: function (data) {
            window.location = Router.action("Excel", "JsonTest", { selectedColumns: selectedColumns, className: className });
            $('#ExportDataDialog').modal('hide');

        },
        error: function (xhr, textStatus, thrownError) {
            alert("資料匯出錯誤");
            alert(xhr + " " + textStatus + " " + thrownError);
        }
    });
}