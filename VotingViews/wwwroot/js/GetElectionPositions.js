const election = document.querySelector('#ElectionId');
if (election != null) {


    election.addEventListener('change', async (e) => {

        const electionSelect = e.target.value.replace(/ /g, "_");


        fetch(`/Contestant/GetPositionsByElectionId/${electionSelect}`, {
            method: "GET",
            credentials: "include"
        })
            .then(res => res.json())
            .then(res => {

                console.log(res);
                const position = document.querySelector('#PositionId');

                position.innerHTML = '<option disabled="" selected="">--Select Position--</option>';

                res.map(op => {
                    let option = document.createElement('option');
                    option.value = op.id;
                    option.textContent = op.name;

                    position.appendChild(option);
                })
            })
    })
}