var dataTable

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tbtCompanyData').DataTable({

        "ajax": {
            url: '/admin/company/getall',
            dataSrc: function (json) {
                console.log(json);  // In ra dữ liệu từ API
                return json.data || [];
            }
        },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "25%" },
            { data: 'industry', "width": "15%" },
            { data: 'email', "width": "15%" },
            { data: 'phone', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class = "w-75 d-flex gap-1" role=""> 
                    <a href="/admin/company/upsert?id=${data}" class="btn btn-sm btn-warning mx-1">
                        <i class="fa-solid fa-pen-to-square"></i> 
                    </a>
                    <a onClick=Delete('/admin/company/hidden/${data}')  class="btn btn-sm btn-danger mx-1">
                        <i class="fa-solid fa-trash"></i> 
                    </a>
                     <a href="/admin/company/detail?id=${data}"  class="btn btn-sm btn-info mx-1">
                        <i class="fa-solid fa-eye"></i> 
                    </a>
                    </div>`;
                },
                "width": "15%"
            }
        ],
       "lengthMenu": [
            [5, 10, 25, -1],
            [5, 10, 25, 'All']
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'PUT',
                success: function (data) {
                    dataTable.ajax.reload();
                    toast.success(data.message)
                }
            })

        
        }
    });
}
