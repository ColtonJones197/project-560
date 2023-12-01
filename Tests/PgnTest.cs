using netapi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class PgnTest
    {
        //it's too long, I know!
        private readonly string pgn = "[Event \"Live Chess\"] [Site \"Chess.com\"] [Date \"2013.10.07\"] [Round \"-\"] [White \"nathan2011\"] [Black \"ImaSpaceCowboy\"] [Result \"1-0\"] [CurrentPosition \"2KQ4/8/4k3/8/8/8/8/8 b - -\"] [Timezone \"UTC\"] [ECO \"B02\"] [ECOUrl \"https://www.chess.com/openings/Alekhines-Defense-Scandinavian-Variation-3.exd5\"] [UTCDate \"2013.10.07\"] [UTCTime \"22:52:02\"] [WhiteElo \"1018\"] [BlackElo \"786\"] [TimeControl \"1800\"] [Termination \"nathan2011 won by resignation\"] [StartTime \"22:52:02\"] [EndDate \"2013.10.07\"] [EndTime \"23:13:19\"] [Link \"https://www.chess.com/game/live/617340446\"]  1. e4 {[%clk 0:29:58.7]} 1... d5 {[%clk 0:29:57]} 2. exd5 {[%clk 0:29:57.2]} 2... Nf6 {[%clk 0:29:53.7]} 3. Nc3 {[%clk 0:29:41.9]} 3... e6 {[%clk 0:29:51.1]} 4. d4 {[%clk 0:29:39.6]} 4... exd5 {[%clk 0:29:47.9]} 5. Bf4 {[%clk 0:29:29.9]} 5... Bb4 {[%clk 0:29:31.4]} 6. Bd2 {[%clk 0:29:18.3]} 6... Nc6 {[%clk 0:28:39.2]} 7. Bb5 {[%clk 0:29:14.3]} 7... O-O {[%clk 0:28:28.5]} 8. Bxc6 {[%clk 0:29:10.9]} 8... bxc6 {[%clk 0:28:26.5]} 9. Nf3 {[%clk 0:29:04]} 9... Bg4 {[%clk 0:28:17.7]} 10. O-O {[%clk 0:29:00.7]} 10... Rb8 {[%clk 0:28:05]} 11. h3 {[%clk 0:28:44.6]} 11... Bf5 {[%clk 0:27:43.1]} 12. Ng5 {[%clk 0:28:33.9]} 12... Re8 {[%clk 0:26:32.8]} 13. Re1 {[%clk 0:28:30.7]} 13... c5 {[%clk 0:25:53.9]} 14. dxc5 {[%clk 0:28:24.8]} 14... d4 {[%clk 0:25:28.2]} 15. Na4 {[%clk 0:27:57.6]} 15... d3 {[%clk 0:24:45.3]} 16. Bxb4 {[%clk 0:27:52.7]} 16... Rxb4 {[%clk 0:24:39.1]} 17. cxd3 {[%clk 0:27:42.4]} 17... Bxd3 {[%clk 0:24:24.2]} 18. Rxe8+ {[%clk 0:26:57.7]} 18... Nxe8 {[%clk 0:24:13]} 19. Qh5 {[%clk 0:26:37.4]} 19... Nf6 {[%clk 0:23:59.8]} 20. Qxf7+ {[%clk 0:26:30.6]} 20... Kh8 {[%clk 0:23:54.6]} 21. Ne6 {[%clk 0:26:19.6]} 21... Qg8 {[%clk 0:23:13.1]} 22. Qxg8+ {[%clk 0:26:13.5]} 22... Kxg8 {[%clk 0:23:10.8]} 23. Nxc7 {[%clk 0:26:11.4]} 23... Rxa4 {[%clk 0:23:08.6]} 24. b3 {[%clk 0:25:55.7]} 24... Re4 {[%clk 0:22:41.2]} 25. Rd1 {[%clk 0:24:59.7]} 25... Be2 {[%clk 0:22:05.4]} 26. Rd8+ {[%clk 0:24:47]} 26... Kf7 {[%clk 0:21:58.1]} 27. f3 {[%clk 0:24:12.6]} 27... Re5 {[%clk 0:21:32.3]} 28. c6 {[%clk 0:23:47.3]} 28... Bb5 {[%clk 0:21:24.3]} 29. Nxb5 {[%clk 0:23:37.2]} 29... Rxb5 {[%clk 0:21:21.3]} 30. c7 {[%clk 0:23:31.5]} 30... Rc5 {[%clk 0:21:18.3]} 31. c8=Q {[%clk 0:23:28]} 31... Rxc8 {[%clk 0:21:09.4]} 32. Rxc8 {[%clk 0:23:26.3]} 32... h5 {[%clk 0:20:58.1]} 33. Rc6 {[%clk 0:23:23.9]} 33... g5 {[%clk 0:20:54.7]} 34. a4 {[%clk 0:23:17.5]} 34... g4 {[%clk 0:20:53.4]} 35. fxg4 {[%clk 0:23:13.8]} 35... hxg4 {[%clk 0:20:52.1]} 36. hxg4 {[%clk 0:23:12.3]} 36... Nxg4 {[%clk 0:20:51]} 37. Rc7+ {[%clk 0:23:10.2]} 37... Kf6 {[%clk 0:20:43.6]} 38. Rxa7 {[%clk 0:23:08.4]} 38... Kf5 {[%clk 0:20:42.4]} 39. b4 {[%clk 0:23:02.2]} 39... Ke5 {[%clk 0:20:40.2]} 40. b5 {[%clk 0:23:01.2]} 40... Nf6 {[%clk 0:20:38]} 41. Rc7 {[%clk 0:22:59.3]} 41... Kd6 {[%clk 0:20:36.2]} 42. b6 {[%clk 0:22:57.4]} 42... Nd7 {[%clk 0:20:34.7]} 43. Rb7 {[%clk 0:22:43.9]} 43... Kc6 {[%clk 0:20:29.6]} 44. a5 {[%clk 0:22:41.5]} 44... Kxb7 {[%clk 0:20:26.7]} 45. Kf2 {[%clk 0:22:37.3]} 45... Ka6 {[%clk 0:20:24.2]} 46. Kf3 {[%clk 0:22:32.5]} 46... Kxa5 {[%clk 0:20:21.8]} 47. Kf4 {[%clk 0:22:30.6]} 47... Kb5 {[%clk 0:20:20.1]} 48. g4 {[%clk 0:22:28.2]} 48... Kxb6 {[%clk 0:20:17.7]} 49. g5 {[%clk 0:22:26.8]} 49... Nf8 {[%clk 0:20:11.6]} 50. Kf5 {[%clk 0:22:25.4]} 50... Kc6 {[%clk 0:20:09.5]} 51. Kf6 {[%clk 0:22:24.2]} 51... Kd7 {[%clk 0:20:08.1]} 52. Kf7 {[%clk 0:22:23.4]} 52... Nh7 {[%clk 0:20:03.8]} 53. g6 {[%clk 0:22:22]} 53... Ng5+ {[%clk 0:19:55]} 54. Kf8 {[%clk 0:22:13.7]} 54... Ke6 {[%clk 0:19:53.5]} 55. g7 {[%clk 0:22:11.9]} 55... Kf6 {[%clk 0:19:30.7]} 56. g8=Q {[%clk 0:22:09.1]} 56... Ne6+ {[%clk 0:19:26.6]} 57. Ke8 {[%clk 0:22:01.6]} 57... Ng7+ {[%clk 0:19:19.1]} 58. Kd8 {[%clk 0:21:58.2]} 58... Ne6+ {[%clk 0:19:10.4]} 59. Kc8 {[%clk 0:21:47.2]} 59... Ke7 {[%clk 0:18:50.8]} 60. Qg1 {[%clk 0:21:20.6]} 60... Nd8 {[%clk 0:18:17.7]} 61. Qg5+ {[%clk 0:21:14.3]} 61... Ke6 {[%clk 0:18:03.4]} 62. Qxd8 {[%clk 0:21:12.5]} 1-0 ";

        public PgnTest()
        {

        }

        [TestMethod]
        public void Pgn_Parse_Should_Parse()
        {
            var result = DataController.GetResultFromPgn(pgn);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Pgn_Parse_Opening_Should_Parse_Openings()
        {
            var result = DataController.GetOpeningFromPgn(pgn);
            Assert.AreEqual("B02", result);
        }
    }
}
