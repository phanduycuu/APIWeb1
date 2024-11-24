function fetchUserDataAndRenderChart(startDate, endDate) {
    $.ajax({
        url: "/statistical/GetUserRegistrations",
        type: "GET",
        data: {
            startDate: startDate,
            endDate: endDate
        },
        success: function (response) {
            if (response && response.labels && response.jobSeekerData && response.employerData) {
                renderChart(response.labels, response.jobSeekerData, response.employerData);
            } else {
                console.error("Dữ liệu trả về không hợp lệ.");
            }
        },
        error: function (xhr, status, error) {
            console.error("Lỗi khi gọi API: ", error);
        }
    });
}

// Hàm vẽ biểu đồ
function renderChart(labels, jobSeekerData, employerData) {
    const ctx = document.getElementById("userChart").getContext("2d");

    // Xóa biểu đồ cũ (nếu có)
    if (window.userChartInstance) {
        window.userChartInstance.destroy();
    }

    // Vẽ biểu đồ mới
    window.userChartInstance = new Chart(ctx, {
        type: "line",
        data: {
            labels: labels, // Ngày
            datasets: [
                {
                    label: "Job Seeker",
                    data: jobSeekerData,
                    borderColor: "orange",
                    backgroundColor: "rgba(255, 165, 0, 0.2)",
                    fill: true
                },
                {
                    label: "Employer",
                    data: employerData,
                    borderColor: "black",
                    backgroundColor: "rgba(0, 0, 0, 0.2)",
                    fill: true
                }
            ]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    position: "top"
                },
                tooltip: {
                    enabled: true
                }
            },
            scales: {
                x: {
                    title: {
                        display: true,
                        text: "Date"
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: "Number of Users"
                    }
                }
            }
        }
    });
}

// Gọi API và hiển thị biểu đồ với dữ liệu mặc định
const defaultStartDate = "2024-04-01";
const defaultEndDate = "2024-04-30";
fetchUserDataAndRenderChart(defaultStartDate, defaultEndDate);

$(document).ready(function () {
    // Khởi tạo Date Range Picker
    $('#dateRange').daterangepicker({
        locale: {
            format: 'YYYY-MM-DD', // Định dạng ngày tháng
            separator: " - ",    // Ký tự ngăn cách ngày bắt đầu và kết thúc
            applyLabel: "Áp dụng",
            cancelLabel: "Hủy",
            fromLabel: "Từ",
            toLabel: "Đến",
            customRangeLabel: "Tùy chọn",
            daysOfWeek: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
            monthNames: [
                "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
                "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"
            ],
            firstDay: 1 // Bắt đầu tuần từ thứ Hai
        },
        // Không giới hạn startDate và endDate
        startDate: moment().subtract(1, 'months'), // Mặc định từ 1 tháng trước
        endDate: moment(),                        // Đến hôm nay
        opens: 'right',                           // Mở lịch ở bên phải
        // Loại bỏ giới hạn thời gian
        minDate: '2000-01-01',                    // Ngày sớm nhất có thể chọn
        maxDate: '2100-12-31',                    // Ngày muộn nhất có thể chọn
    }, function (start, end, label) {
        console.log("Khoảng ngày đã chọn: " + start.format('YYYY-MM-DD') + " đến " + end.format('YYYY-MM-DD'));
    });

    // Sự kiện khi người dùng chọn ngày và nhấn "Áp dụng"
    $('#dateRange').on('apply.daterangepicker', function (ev, picker) {
        const startDate = picker.startDate.format('YYYY-MM-DD');
        const endDate = picker.endDate.format('YYYY-MM-DD');
        console.log("Người dùng đã chọn: ", startDate, endDate);

        // Gọi API để cập nhật dữ liệu biểu đồ
        fetchUserDataAndRenderChart(startDate, endDate);
    });
});

$.ajax({
    url: "/statistical/GetTotal",
    type: 'GET',
    success: function (response) {
        // Kiểm tra xem dữ liệu trả về có thuộc tính "data" không
        if (response && response.data) {
            const data = response.data;
            // Gán giá trị vào các thẻ h6 tương ứng
            $("#jobseeker").text(data.jobseeker || 0); // Tổng số Job Seeker
            $("#employer").text(data.employer || 0);  // Tổng số Employer
            $("#jobpost").text(data.jobpost || 0);    // Tổng số Job Post
            $("#apply").text(data.apply || 0);        // Tổng số Apply
        } else {
            console.error("Dữ liệu trả về không chứa thuộc tính 'data'.");
        }
    },
    error: function (xhr, status, error) {
        console.error("Lỗi khi gọi API: ", error);
    }
});


        
       