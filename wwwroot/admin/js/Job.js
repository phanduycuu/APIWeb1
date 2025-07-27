var dataTable

$(document).ready(function () {
    loadDataTable();
});

loadDataTable = function () {
    dataTable = $('#tbtJobData').DataTable({
        "ajax": {
            url: '/admin/job/getall',
            dataSrc: function (json) {
                // Check the actual structure from the console log
                console.log(json);  // Analyze the response structure here
                return json.data || []; // Assuming data is within a "data" property
            }
        },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'title', "width": "25%" },
            { data: 'employerEmail', "width": "15%" },
            { data: 'companyName', "width": "15%" },
            { data: 'jobStatus', "width": "15%" }, // Check if the API uses "jobStatus" or something else
            {
                data: 'id',
                "render": function (data) {
                    return `<div class = "w-75 d-flex gap-1" role=""> 
                  <a href="/admin/job/Update?id=${data}" class="btn btn-sm btn-warning mx-1">
                    <i class="fa-solid fa-pen-to-square"></i> 
                  </a>
                  <a onClick=Delete('/admin/job/hidden/${data}')  class="btn btn-sm btn-danger mx-1">
                    <i class="fa-solid fa-trash"></i> 
                  </a>
                  <a href="/admin/job/detail?id=${data}"  class="btn btn-sm btn-info mx-1">
                    <i class="fa-solid fa-eye"></i> 
                  </a>
                </div>`;
                },
                "width": "15%"
            }
        ]
    });
};
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
