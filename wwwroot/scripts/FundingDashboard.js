// Funding Dashboard Scripts


//< !--Main entry-- >
$(document).ready(function () {
    InitialiseTable();
});


function InitialiseTable() {

    var table = $('#funding').DataTable({


        responsive: {
            details: {

                display: $.fn.dataTable.Responsive.display.modal({
                    header: function (row) {
                        var data = row.data();
                        return 'Details for ' + data[0] + ' ' + data[1];
                    }
                }),
                renderer: $.fn.dataTable.Responsive.renderer.tableAll({
                    tableClass: 'table'
                })
            }
        },

        columnDefs:
            [
                { className: "mobile-p", "targets": [6] },

                {
                    targets: [1],
                    visible: true,
                    searchable: true
                },

                {
                    targets: [7, 8, 9],
                    visible: false,
                    searchable: false
                },


                // {
                //     targets: 0,
                //     orderable: false,
                //     visible: true,
                //     className: 'dtr-control'
                // }

            ],




    });
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
