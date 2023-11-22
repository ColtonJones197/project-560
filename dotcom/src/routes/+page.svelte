<script>
	import Counter from './Counter.svelte';
	import welcome from '$lib/images/svelte-welcome.webp';
	import welcome_fallback from '$lib/images/svelte-welcome.png';
    import { onMount } from 'svelte';

	let users = [{username: 'Me'}];
	let loading = true;
	let infoText = "Loading Players...";

	onMount(async () => {
		let playersRoute = 'https://localhost:7127/api/' + 'Player/AllPlayers';
		let userJson = await (await fetch(playersRoute)).json()
			.catch(() => {
				console.log(`Failed to retrieve players`);
				infoText = "failed";
			});
		loading = false;
		users = userJson;
	});
</script>

<svelte:head>
	<title>ChessDB | Home</title>
	<meta name="description" content="Svelte demo app" />
</svelte:head>

<section>
	<h1>
		<span class="welcome">
			<h1 class="text-8xl">WELCOME</h1>
		</span>
	</h1>
	{#if loading}
		<h2>
			{infoText}
		</h2>
	{/if}
	<h2 class="text-4xl">
		Search for a player:
	</h2>

	<h2 class="text-4xl">
		Here's all the players lol
	</h2>

	{#if users !== null}
        <ul class="collection">
            {#each users as player}
                <li class="collection-item font-semibold">{player.username}</li>
            {/each}
        </ul>
    {/if}
</section>

<style>
	section {
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		flex: 0.6;
	}

	h1 {
		width: 100%;
	}

	.welcome {
		display: block;
		position: relative;
		width: 100%;
		height: 0;
		padding: 0 0 calc(100% * 495 / 2048) 0;
	}
</style>
