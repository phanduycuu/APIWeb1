var dataTable

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tbtBlogData').DataTable({

        "ajax": {
            url: '/blog/getall',
            dataSrc: function (json) {
                console.log(json);  // In ra dữ liệu từ API
                return json.data || [];
            }
        },
        "columns": [
            { data: 'title', "width": "25%", "className": "text-center" },
            { data: 'employername', "width": "25%", "className": "text-center" },
            {
                data: 'status',
                "width": "15%",
                "className": "text-center",
                "render": function (data) {
                    if (data === 0) {
                        return "Đang đợi duyệt";
                    } else if (data === 1) {
                        return "Đã duyệt";
                    } else if (data === 2) {
                        return "Từ chối";
                    }
                    return "";
                }
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 d-flex justify-content-center align-items-center gap-1" role=""> 
                  
                     <a href="/Blog/detail?id=${data}"  class="btn btn-sm btn-info mx-1">
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

