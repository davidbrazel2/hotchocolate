using StrawberryShake.Tools.Configuration;
using Xunit;
using static StrawberryShake.CodeGeneration.CSharp.GeneratorTestHelper;

namespace StrawberryShake.CodeGeneration.CSharp.Integration
{
    public class TestGeneration
    {
        [Fact]
        public void StarWarsGetHero() =>
            AssertStarWarsResult(
                CreateIntegrationTest(),
                @"query GetHero {
                    hero(episode: NEW_HOPE) {
                        name
                    }
                }");

        [Fact]
        public void StarWarsGetFriendsNoStore() =>
            AssertStarWarsResult(
                CreateIntegrationTest(noStore: true),
                @"query GetHero {
                    hero(episode: NEW_HOPE) {
                        name
                        friends {
                            nodes {
                                name
                            }
                        }
                    }
                }");

        [Fact]
        public void StarWarsGetFriends() =>
            AssertStarWarsResult(
                CreateIntegrationTest(),
                @"query GetHero {
                    hero(episode: NEW_HOPE) {
                        name
                        friends {
                            nodes {
                                name
                            }
                        }
                    }
                }");

        [Fact]
        public void MultiProfile() =>
            AssertStarWarsResult(
                CreateIntegrationTest(profiles: new[]
                {
                    new TransportProfile("InMemory", TransportType.InMemory),
                    TransportProfile.Default
                }),
                @"query GetHero {
                    hero(episode: NEW_HOPE) {
                        name
                        friends {
                            nodes {
                                name
                            }
                        }
                    }
                }",
                @"subscription OnReviewSub {
                    onReview(episode: NEW_HOPE) {
                      __typename
                      stars
                      commentary
                    }
                  }
                ",
                @"mutation createReviewMut($episode: Episode!, $review: ReviewInput!) {
                    createReview(episode: $episode, review: $review) {
                        stars
                        commentary
                    }
                }");

        [Fact]
        public void StarWarsTypeNameOnInterfaces() =>
            AssertStarWarsResult(
                CreateIntegrationTest(),
                @"query GetHero {
                    hero(episode: NEW_HOPE) {
                        __typename
                    }
                }");

        [Fact]
        public void StarWarsTypeNameOnUnions() =>
            AssertStarWarsResult(
                CreateIntegrationTest(),
                @"query SearchHero {
                    search(text: ""l"") {
                        __typename
                    }
                }");

        [Fact]
        public void StarWarsUnionList() =>
            AssertStarWarsResult(
                CreateIntegrationTest(),
                @"query SearchHero {
                    search(text: ""l"") {
                        ... on Human {
                            name
                            friends {
                                nodes {
                                    name
                                }
                            }
                        }
                        ... on Droid {
                            name
                        }
                    }
                }");
    }
}
