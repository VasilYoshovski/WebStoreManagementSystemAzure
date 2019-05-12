var usedProd = [];
var reloadDialog = true;
var forDelProd = [];

function fillUsedDbIds() {
    var table = document.getElementById('productsTable');
    var targetTDs = table.querySelectorAll('tr > td[name=dbidname]');
    for (var i = 0; i < targetTDs.length; i++) {
        var td = targetTDs[i];
        usedProd.push(parseInt(td.innerHTML));
    }
}

$(function () {
    $("#dialog").dialog(
        { autoOpen: false },
        { modal: true },
        { show: { effect: "scale", duration: 300 } },
        {
            close: function (event, ui) {
                if (reloadDialog) location.reload();
            }
        }

    );
});

function pushToDB() {
    var productList = [];
    var table = document.getElementById('productsTable');
    var idTDs = table.querySelectorAll('tr > td[name=idname]');
    var quaTDs = table.querySelectorAll('tr > td[name = quantitytdname] > input[name=quantityname]');
    //alert(quaTDs.length);

    for (var i = 0; i < idTDs.length; i++) {
        var idp = parseInt(idTDs[i].innerHTML);
        var quap = parseFloat(quaTDs[i].value);

        if (isNaN(idp) || isNaN(quap)) {

            reloadDialog = false;
            $("#dialog").dialog("open");
            $("#dialogtext").text("Not a number value");

            return false;
        }

        else {
            productList.push({ "ProductId": idp, "Quantity": quap })

        }
        //alert('med');
    }

    if (productList.length > 0) {

        var modelg = {
            SOId: $('#idfield').text(),
            Products: productList
        }
        //"KliombaUrl.Action("AddProductsToSale", "ProductSale")"
        $.ajax({
            method: 'post',
            url: "/Product" + tablename + "/AddProductsTo" + tablename,
            data: JSON.stringify(modelg),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
                reloadDialog = data.isSucceded;
                $("#dialog").dialog("open");
                $("#dialogtext").text(data.text);
            }
        });
    } else {
        reloadDialog = false;
        $("#dialog").dialog("open");
        $("#dialogtext").text("No new products to add to current " + tablename);
    }
}

function showSearch() {
    document.getElementById("divlist").style.display = "inline";
    document.getElementById("addrowButton").disabled = true;
}

function deleteDbRow(r) {
    var row = r.parentNode.parentNode.parentNode;
    var i = row.rowIndex;
    var a = document.getElementById("productsTable").rows[i].cells[0].innerHTML;
    if (!isNaN(a)) {
        forDelProd.push(a);

    }
    document.getElementById("productsTable").deleteRow(i);
}

function deleteRow(r) {
    var row = r.parentNode.parentNode.parentNode;
    var i = row.rowIndex;
    var a = document.getElementById("productsTable").rows[i].cells[0].innerHTML;
        
    if (!isNaN(a)) {
        usedProd.pop(a);
    }
    document.getElementById("productsTable").deleteRow(i);
}

function addRow() {
    var parentTable = document.getElementById('productsTable');
    var myTd, myInput;
    var myTr = document.createElement('tr');
    //for (var i = 0; i < 5; i++)
    {
        myTd = document.createElement('td');
        myTd.setAttribute('name', 'idname');
        myTd.setAttribute('class', 'center');
        myTd.innerHTML = "-";
        myTr.appendChild(myTd);

        myTd = document.createElement('td');
        myTd.innerHTML = "-";
        myTr.appendChild(myTd);

        myTd = document.createElement('td');
        myTd.innerHTML = "-";
        myTr.appendChild(myTd);

        myTd = document.createElement('td');
        myTd.setAttribute('name', 'quantitytdname');
        myInput = document.createElement('input');
        myInput.setAttribute('type', 'text');
        myInput.setAttribute('class', 'quantityfield');
        myInput.setAttribute('name', 'quantityname');
        myInput.setAttribute('size', 5);
        myTd.appendChild(myInput);
        myTr.appendChild(myTd);

        myTd = document.createElement('td');
        myTd.innerHTML = "-";
        myTd.setAttribute('name', 'pricename');
        myTr.appendChild(myTd);

        myTd = document.createElement('td');
        myTd.innerHTML = "-";
        myTd.setAttribute('name', 'pricenamewd');
        myTr.appendChild(myTd);

        myTd = document.createElement('td');
        myTd.innerHTML = "-";
        myTd.setAttribute('name', 'totalname');
        myTr.appendChild(myTd);

        //<td align="right">
        //<a href=""><button class="stakbutton"><span>Details</span></button></a>
        //<a><button class="stakbutton" onclick="deleteDbRow(this)"><span>Delete</span></button></a>
        //</td>

        myTd = document.createElement('td');
        myTd.setAttribute('align', 'right');
        myA = document.createElement('a');
        myA.setAttribute('href', '/Products/');
        myBut = document.createElement('button');
        myBut.setAttribute('class', 'stakbutton')
        mySpan = document.createElement('span');
        mySpan.innerHTML = "Details";
        myBut.appendChild(mySpan);
        myA.appendChild(myBut);
        myTd.appendChild(myA);

        myA2 = document.createElement('a');
        myBut2 = document.createElement('button');
        myBut2.setAttribute('class', 'stakbutton')
        myBut2.setAttribute('onclick', 'deleteRow(this)')
        mySpan2 = document.createElement('span');
        mySpan2.innerHTML = "Delete";
        myBut2.appendChild(mySpan2);
        myA2.appendChild(myBut2);
        myTd.appendChild(myA2);

        myTr.appendChild(myTd);
    }
    parentTable.appendChild(myTr);
}

$(document).ready(function () {
    GetTotal();
    fillUsedDbIds();
    $("#productList").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/products/ProductList',
                type: 'GET',
                cache: false,
                data: request,
                dataType: 'json',
                success: function (data) {
                    response($.map(data, function (item) {
                        //alert(item.productId + ':' + item.name);
                        if (!usedProd.includes(item.productId)) {
                            return {
                                label: item.name,
                                value: item.productId
                            }
                        }
                    }))
                }
            });
        },
        minLength: 2,
        delay: 0,
        select: function (event, ui) {
            $('#productList').val(ui.item.label);
            addRow();
            getProduct(ui.item.value);

            document.getElementById("productList").value = '';
            document.getElementById("divlist").style.display = 'none';
            document.getElementById("addrowButton").disabled = false;

            usedProd.push(ui.item.value);

            return false;
        }
    });
});

function getProduct(id) {
    var options = {};
    options.url = "/products/ProductData";
    options.type = "GET";
    options.data = { "id": id };
    options.dataType = "json";
    options.success = function (data) {
        var c = parseFloat($('#productdiscount').text());
        var rowCount = $('#productsTable tr').length;
        document.getElementById("productsTable").rows[rowCount - 1].cells[0].innerHTML = data.productID;
        document.getElementById("productsTable").rows[rowCount - 1].cells[1].innerHTML = data.name;
        document.getElementById("productsTable").rows[rowCount - 1].cells[2].innerHTML = data.measure;
        document.getElementById("productsTable").rows[rowCount - 1].cells[4].innerHTML = data.retailPrice.toFixed(2);
        document.getElementById("productsTable").rows[rowCount - 1].cells[5].innerHTML = (data.retailPrice*(1-c/100)).toFixed(2);
        document.getElementById("productsTable").rows[rowCount - 1].cells[6].innerHTML = "-";

    };

    $.ajax(options);
}

$(document).on('keyup', '#productsTable input.quantityfield', function () {
    var tr = $(this).closest('tr');
    var a = parseFloat(tr.find('td[name=pricename]').text());
    var b = parseFloat(tr.find('input[name=quantityname]').val());
    var c = parseFloat($('#productdiscount').text());
    //alert('Table 1: ' + a + ' '+c +' '+b);
    if (!isNaN(a) && !isNaN(b) && !isNaN(c) && b > 0) {
        var res = a * b * (1 - (c / 100));
        tr.find('td[name=totalname]').text(res.toFixed(2));
        GetTotal();
    }
    else {
        tr.find('td[name=totalname]').text('-');

    }
});

function GetTotal() {
    var table = document.getElementById('productsTable');
    var targetTDs = table.querySelectorAll('tr > td[name=totalname]');
    var res = 0;
    for (var i = 0; i < targetTDs.length; i++) {
        var td = targetTDs[i];
        var tot = parseFloat(td.innerHTML);
        //alert(tot);

        //   if (!isNaN(tot)) {
        res = res + tot;
        //  }
        //alert(res);
    }
    if (!isNaN(res)) {
        $('#totalnovat').text(res.toFixed(2));
        $('#vattotal').text((res * 0.2).toFixed(2));
        $('#totalwithvat').text((res * 1.2).toFixed(2));
        //alert(res);
    }
    else {
        $('#totalnovat').text('');
        $('#vattotal').text('');
        $('#totalwithvat').text('');
    }
}