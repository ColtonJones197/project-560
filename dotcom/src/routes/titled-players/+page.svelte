<script>
    let players = ['Me', 'Myself', 'I'];
    let baseUrl = 'https://api.chess.com/';
    let endpointUrl = 'pub/titled/GM';
    let buttonText = 'Click Me!';

    let fetchButtonClick = (async () => {
        buttonText = 'Loading...';
        const res = await fetch(`${baseUrl + endpointUrl}`);
        let jsonObject = await res.json()
            .catch(() => {
                console.log('Data retrieval failure');
            });
        players = jsonObject.players;
        buttonText = 'Retrieval Complete';
    });

</script>



<div>
    <h1>List of titled players</h1>
    <button class="waves-effect waves-light btn" aria-roledescription="getter" on:click={fetchButtonClick}>{buttonText}</button>
    {#if players !== null}
        <ul class="collection">
            {#each players as player}
                <li class="collection-item">{player}</li>
            {/each}
        </ul>
    {/if}
</div>