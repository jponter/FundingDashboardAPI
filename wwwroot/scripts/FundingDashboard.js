// Funding Dashboard Scripts

//set localhost to true for local debug in visual studio
//SET to FALSE FOR RELEASE!
var localhost = true;
$('#funding').hide();

//< !--Main entry-- >
$(document).ready(function () {
    
    loadFundingAll();
    //InitialiseTable();

});


function InitialiseTable() {

              
    $('#funding').DataTable({

        
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
       

    $('#funding').show();    
    
}


function loadFundingAll() {

    if (localhost) {
        var fundingURL = "http://localhost:8080/api/Funding";
    } else {
        var fundingURL = "https://careerframeworkapi.azurewebsites.net/api/Professions";
    }



    try {
        //get the funding
        $.getJSON(fundingURL, function (data, status) {
            console.log("Status: " + status);
            if (status == "success") {
                console.log("getJSON-Funding:");
                console.log("URL = " + fundingURL);
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



                    //append to #funding

                    //<th>Funding Name</th>
                    //<th>CSP</th>
                    //<th>Region</th>
                    //<th>Service Line</th>
                    //<th>Minimum Criteria</th>
                    //<th>Description</th>
                    //<th>How to Apply</th>
                    //<th>When to Apply</th>
                    //<th>Approval Process</th>
                    //<th>Funding Collection Process</th>

                    var newReqRow = '<tr><td>' + obj.name + '</td><td> ' + obj.csp + '</td ><td>' + obj.serviceLine +
                        '<td> ' + obj.minimumCriteria + '</td> ' +
                        '<td> ' + obj.description + '</td> ' +
                        '<td>' + obj.howToApply + '</td>' +
                        '<td> ' + obj.whenToApply + '</td> ' +
                        '<td> ' + obj.approvalProcess + '</td> ' +
                        '<td> ' + obj.fundingCollection + '</td> ' +
                        '<td> ' + obj.addedBy + '</td> ' +
                        '<td> ' + d.toLocaleDateString() + '</td> ' +
                        '<td> ' + obj.uk + '</td> ' +
                        '<td> ' + obj.usa + '</td> ' +
                        '<td> ' + obj.eur + '</td> ' +
                        '<td> ' + obj.asia + '</td></tr>';


                    $("#funding").append(newReqRow);

                    //$("#funding tbody").append("<tr><td>" + obj.name + "</td>< td > " + "data" + "</td >< td >" + obj.serviceLine + "</td ></tr >");




                    //href="#" class= "list-group-item list-group-item-action" id = ' + obj.skillCode + ' > ' + text + '</a > ');


                });


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

    console.log("init table begin time wait");
    setTimeout(InitialiseTable, 1000);
    console.log("init table end time wait");
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
