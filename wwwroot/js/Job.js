var dataTable

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tbtJobData').DataTable({

        "ajax": {
            url: '/job/getall',
            dataSrc: function (json) {
                console.log(json);  // In ra dữ liệu từ API
                return json.data || [];
            }
        },
        "columns": [
            { data: 'title', "width": "16%", "className": "text-center" },
            { data: 'employerName', "width": "16%", "className": "text-center" },
            { data: 'jobLevel', "width": "10%", "className": "text-center" },
            { data: 'jobType', "width": "10%", "className": "text-center" },
            { data: 'jobStatus', "width": "15%", "className": "text-center" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 d-flex justify-content-center align-items-center gap-1" role=""> 
                  
                     <a href="/job/detail?id=${data}"  class="btn btn-sm btn-info mx-1">
                        <i class="fa-solid fa-eye"></i> 
                    </a>
                    </div>`;
                },
                "width": "9%"
            }
        ],
       "lengthMenu": [
            [5, 10, 25, -1],
            [5, 10, 25, 'All']
        ]
    });
}

