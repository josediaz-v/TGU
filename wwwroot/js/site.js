function confirmRemovePicture() {
    return confirm("Are you sure you want to remove this picture?");
}
function openModal(pictureUrl) {
    var modal = document.getElementById('pictureModal');
    var modalImage = document.getElementById('modalImage');
    modalImage.src = pictureUrl;
    modal.style.display = 'block';
}

function closeModal() {
    var modal = document.getElementById('pictureModal');
    modal.style.display = 'none';
}

function confirmRemovePicture() {
    return confirm("Are you sure you want to remove this picture?");
}