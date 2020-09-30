// Funding Dashboard Scripts

//set localhost to true for local debug in visual studio
//SET to FALSE FOR RELEASE!
var localhost = true;
var serverData = null;

var table = null;
//$('#funding').hide();

if (localhost) {
    var fundingURL = "http://localhost:8080/api/Funding";
    var resetURL = fundingURL;
} else {
    var fundingURL = "https://careerframeworkapi.azurewebsites.net/api/Professions";
}

//< !--Main entry-- >
$(document).ready(function () {

    InitialiseTable();
    loadFundingReset();
    //InitialiseTable();

});


function InitialiseTable() {

              
    table = $('#funding').DataTable({
        data: serverData,
        lengthMenu: [5,10,15,20,25],
        columns: [
            { data: "name" },
            { data: "csp" },
            { data: "serviceLine" },
            { data: "minimumCriteria" },
            { data: "description" },
            { data: "howToApply" },
            { data: "whenToApply" },
            { data: "approvalProcess" },
            { data: "fundingCollection" },
            { data: "addedBy" },
            { data: "addedOn" },
            { data: "uk" },
            { data: "usa" },
            { data: "eur" },
            {data: "asia"}

            
                    //    '<td> ' + obj.whenToApply + '</td> ' +
                    //    '<td> ' + obj.approvalProcess + '</td> ' +
                    //    '<td> ' + obj.fundingCollection + '</td> ' +
                    //    '<td> ' + obj.addedBy + '</td> ' +
                    //    '<td> ' + d.toLocaleDateString() + '</td> ' +
                    //    '<td> ' + obj.uk + '</td> ' +
                    //    '<td> ' + obj.usa + '</td> ' +
                    //    '<td> ' + obj.eur + '</td> ' +
                    //    '<td> ' + obj.asia + '</td></tr>';

        ],

        responsive: {
            details: {

                display: $.fn.dataTable.Responsive.display.modal({
                    header: function (row) {
                        var data = row.data();
                        return 'Details for ' + data[0];
                            //+ ' CSP: ' + data[1];
                    }
                }),
                renderer: $.fn.dataTable.Responsive.renderer.tableAll({
                    tableClass: 'table'
                })
            }
        },
        

        columnDefs:
            [
                { className: "mobile-p", "targets": [3] },

                {
                    targets: 10,
                    render: $.fn.dataTable.render.moment("YYYY-MM-DDTHH:mm:ss","DD-MM-YYYY")
                },

                {
                    targets: [1,2,3,4,5],
                    visible: true,
                    searchable: true
                },

                {
                    targets: [6,7,8,11,12,13,14],
                    visible: false,
                    searchable: false
                },


                // {
                //     targets: 0,
                //     orderable: false,
                //     visible: true,
                //     className: 'dtr-control'
                // }

            ]
        


    });
       

    //$('#funding').show();    
    
}


function loadFundingReset() {
    try {
        //get the funding
        $.getJSON(resetURL, function (data, status) {
            console.log("Status: " + status);
            if (status == "success") {
                console.log("getJSON-Funding:");
                console.log("URL = " + resetURL);
                console.log(data);

                serverData = data;

                $(data).each(function (index, obj) {

                    //var text = $.trim(obj.skillText);
                    console.log("Ob: " + obj.id);
                    console.log("ObName: " + obj.name);
                    var csp = $.trim(obj.csp);
                    console.log("ObCSP: " + csp);
                    var serviceLine = $.trim(obj.serviceLine);
                    console.log("ObCSP: " + serviceLine);

                    var isoDate = obj.addedOn;
                    var d = new Date(isoDate);





                });

                var datatable = $('#funding').DataTable();

                //change the datatable data
                datatable.clear();
                datatable.rows.add(data);
                datatable.draw();
                //InitialiseTable();
            } else {
                alert("Database Connection Error");
                alert = true;
            }


        });

    }
    catch (e) {
        alert(e);
        console.log("error getting funding");
    }

   
}

function loadFundingCSP(csp) {

    try {
        //get the funding
        $.getJSON(fundingURL + "/byCSP?CSP=" + csp, function (data, status) {
            console.log("Status: " + status);
            if (status == "success") {
                console.log("getJSON-Funding:");
                console.log("URL = " + fundingURL + "/byCSP?CSP=" + csp);
                console.log(data);

                $(data).each(function (index, obj) {

                    //var text = $.trim(obj.skillText);
                    console.log("Ob: " + obj.id);
                    console.log("ObName: " + obj.name);
                    var csp = $.trim(obj.csp);
                    console.log("ObCSP: " + csp);
                    var serviceLine = $.trim(obj.serviceLine);
                    console.log("ObCSP: " + serviceLine);

                    var isoDate = obj.addedOn;
                    var d = new Date(isoDate);

                });


                var datatable = $('#funding').DataTable();

                //change the datatable data
                datatable.clear();
                datatable.rows.add(data);
                datatable.draw();


            } else {
                alert("Database Connection Error");
                alert = true;
            }


        });

    }
    catch (e) {
        alert(e);
        console.log("error getting funding");
    }


}

function loadFundingSL(SL) {

    try {
        //get the funding
        $.getJSON(fundingURL + "/bySL?SL=" + SL, function (data, status) {
            console.log("Status: " + status);
            if (status == "success") {
                console.log("getJSON-Funding:");
                console.log("URL = " + fundingURL + "/bySL?SL=" + SL);
                console.log(data);

                $(data).each(function (index, obj) {

                    //var text = $.trim(obj.skillText);
                    console.log("Ob: " + obj.id);
                    console.log("ObName: " + obj.name);
                    var csp = $.trim(obj.csp);
                    console.log("ObCSP: " + csp);
                    var serviceLine = $.trim(obj.serviceLine);
                    console.log("ObCSP: " + serviceLine);

                    var isoDate = obj.addedOn;
                    var d = new Date(isoDate);

                });


                var datatable = $('#funding').DataTable();

                //change the datatable data
                datatable.clear();
                datatable.rows.add(data);
                datatable.draw();


            } else {
                alert("Database Connection Error");
                alert = true;
            }


        });

    }
    catch (e) {
        alert(e);
        console.log("error getting funding");
    }


}

function loadFundingGeo(geo) {

    try {
        //get the funding
        $.getJSON(fundingURL + "/byRegion?Region=" + geo, function (data, status) {
            console.log("Status: " + status);
            if (status == "success") {
                console.log("getJSON-Funding:");
                console.log("URL = " + fundingURL + "/byRegion?Region=" + geo);
                console.log(data);

                $(data).each(function (index, obj) {

                    //var text = $.trim(obj.skillText);
                    console.log("Ob: " + obj.id);
                    console.log("ObName: " + obj.name);
                    var csp = $.trim(obj.csp);
                    console.log("ObCSP: " + csp);
                    var serviceLine = $.trim(obj.serviceLine);
                    console.log("ObCSP: " + serviceLine);

                    var isoDate = obj.addedOn;
                    var d = new Date(isoDate);

                });


                var datatable = $('#funding').DataTable();

                //change the datatable data
                datatable.clear();
                datatable.rows.add(data);
                datatable.draw();


            } else {
                alert("Database Connection Error");
                alert = true;
            }


        });

    }
    catch (e) {
        alert(e);
        console.log("error getting funding");
    }


}



//clear table
var clearFundingTable = function () {
    console.log("Clear #funding");
    $("#funding tbody tr").remove();
}


function AddFooterFilter() {
    // var table = $('#funding').DataTable();
    console.log("Flatten and search");

    table.columns().flatten().each(function (colIdx) {
        // Create the select list and search operation
        var select = $('<select />')
            .appendTo(
                table.column(colIdx).footer()
            )
            .on('change', function () {
                table
                    .column(colIdx)
                    .search($(this).val())
                    .draw();
            });

        // Get the search data for the first column and add to the select list
        table
            .column(colIdx)
            .cache('search')
            .sort()
            .unique()
            .each(function (d) {
                select.append($('<option value="' + d + '">' + d + '</option>'));
            });
    });
}
