<script>
    import { onMount } from "svelte";


    let players = ['Me', 'Myself', 'I'];
    let baseUrl = 'https://api.chess.com/';
    let endpointUrl = 'pub/titled/GM';
    let buttonText = 'Click Me!';

    onMount(async () => {
        let mRes = await fetch(`https://api.chess.com/pub/titled/FM`);
        let initialJson = await mRes.json()
            .catch(() => {
                console.log('Failed to retrieve list of masters...');
            });
        players = initialJson.players;
    })

    let fetchButtonClick = (async () => {
        buttonText = 'Loading...';
        const res = await fetch(`${baseUrl + endpointUrl}`);
        let jsonObject = await res.json()
            .catch(() => {
                console.log('Data retrieval failure');
            });
        players = jsonObject.players;
        players.forEach((player) => {
            console.log(player);
        })
        buttonText = 'Retrieval Complete';
    });

</script>



<div>
    <h1>List of titled players</h1>
    <button class="waves-effect waves-light btn" aria-roledescription="getter" on:click={fetchButtonClick}>{buttonText}</button>
    {#if players !== null}
        <ul class="collection">
            {#each players as player}
                <li class="collection-item font-semibold">{player}</li>
            {/each}
        </ul>
    {/if}
</div>
