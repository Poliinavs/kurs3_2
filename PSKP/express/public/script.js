
async function Delete(){ 
    const id=document.getElementById("FIO").value
    console.log(id)
    const response=await fetch(`/delete/${id}`,{
        method:'POST'
    });
    window.location.href='/'
}
function Change() {
    document.getElementById("Delete").disabled = true;
}
