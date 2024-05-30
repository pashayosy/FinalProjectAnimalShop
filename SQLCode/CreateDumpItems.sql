-- Insert Categories
ALTER TABLE Animals AUTO_INCREMENT = 1;

ALTER TABLE Categories AUTO_INCREMENT = 1;

ALTER TABLE Comments AUTO_INCREMENT = 1;

-- Insert Categories
INSERT INTO
    Categories (Name)
VALUES
    ('Mammal'),
    ('Reptile'),
    ('Aquatic'),
    ('Bird'),
    ('Amphibian');

-- Insert Animals
INSERT INTO
    Animals (Name, Age, PictureUrl, Description, CategoryId)
VALUES
    (
        'Lion',
        5,
        'https://upload.wikimedia.org/wikipedia/commons/thumb/7/73/Lion_waiting_in_Namibia.jpg/1200px-Lion_waiting_in_Namibia.jpg',
        'King of the Jungle',
        1
    ),
    (
        'Tiger',
        3,
        'https://files.worldwildlife.org/wwfcmsprod/images/Tiger_resting_Bandhavgarh_National_Park_India/hero_small/6aofsvaglm_Medium_WW226365.jpg',
        'The largest cat species',
        1
    ),
    (
        'Elephant',
        10,
        'https://cdn.britannica.com/02/152302-050-1A984FCB/African-savanna-elephant.jpg',
        'The largest land animal',
        1
    ),
    (
        'Crocodile',
        7,
        'https://cdn.britannica.com/84/198884-050-A37B8971/crocodile-Nile-swath-one-sub-Saharan-Africa-Madagascar.jpg',
        'A large aquatic reptile',
        2
    ),
    (
        'Gecko',
        2,
        'https://www.jardiner-malin.fr/wp-content/uploads/2022/12/Gecko.jpg',
        'A small lizard',
        2
    ),
    (
        'Goldfish',
        1,
        'https://upload.wikimedia.org/wikipedia/commons/thumb/2/25/Common_goldfish.JPG/800px-Common_goldfish.JPG',
        'A small ornamental fish',
        3
    ),
    (
        'Shark',
        8,
        'https://upload.wikimedia.org/wikipedia/commons/0/08/Corl0207_%2828225976491%29.jpg',
        'A large predatory fish',
        3
    ),
    (
        'Parrot',
        4,
        'https://i.natgeofe.com/n/e3ae5fbf-ddc9-4b18-8c75-81d2daf962c1/3948225.jpg',
        'A colorful bird',
        4
    ),
    (
        'Eagle',
        6,
        'https://www.birdlife.org/wp-content/uploads/2021/06/Eagle-in-flight-Richard-Lee-Unsplash-1-edited-scaled.jpg',
        'A bird of prey',
        4
    ),
    (
        'Frog',
        1,
        'https://cdn.mos.cms.futurecdn.net/39CUYMP8vJqHAYGVzUghBX-1200-80.jpg',
        'A small amphibian',
        5
    ),
    (
        'Salamander',
        3,
        'https://upload.wikimedia.org/wikipedia/commons/b/b2/SpottedSalamander.jpg',
        'A small amphibian',
        5
    ),
    (
        'Rabbit',
        2,
        'https://upload.wikimedia.org/wikipedia/commons/thumb/1/1f/Oryctolagus_cuniculus_Rcdo.jpg/1200px-Oryctolagus_cuniculus_Rcdo.jpg',
        'A small mammal',
        1
    ),
    (
        'Hamster',
        1,
        'https://assets.petco.com/petco/image/upload/f_auto,q_auto/hamster-care-sheet-hero',
        'A small rodent',
        1
    ),
    (
        'Turtle',
        50,
        'https://www.fisheries.noaa.gov/s3//styles/full_width/s3/dam-migration/hawksbill_sea_turtle.jpg?itok=ESbU98wo',
        'A reptile with a shell',
        2
    ),
    (
        'Clownfish',
        2,
        'https://www.aquariumofpacific.org/images/made_new/images-uploads-clownfish_600_q85.jpg',
        'A small tropical fish',
        3
    );

-- Insert Comments
INSERT INTO
    Comments (Text, AnimalId)
VALUES
    ('Amazing animal!', 1),
    ('So majestic!', 1),
    ('Love it!', 1),
    ('Scary but cool.', 2),
    ('Beautiful stripes.', 2),
    ('So powerful.', 2),
    ('Very intelligent.', 3),
    ('Love elephants!', 3),
    ('Such a gentle giant.', 3),
    ('Fascinating creature.', 4),
    ('So dangerous.', 4),
    ('Very interesting.', 4),
    ('Cute and small.', 5),
    ('Very agile.', 5),
    ('Love the colors.', 5),
    ('Very calming.', 6),
    ('Nice to watch.', 6),
    ('Peaceful fish.', 6),
    ('Scary but fascinating.', 7),
    ('Very powerful.', 7),
    ('Top of the food chain.', 7),
    ('So colorful!', 8),
    ('Very talkative.', 8),
    ('Can mimic sounds.', 8),
    ('Majestic bird.', 9),
    ('Sharp eyesight.', 9),
    ('A true predator.', 9),
    ('Cute and jumpy.', 10),
    ('Very small.', 10),
    ('Interesting skin.', 10),
    ('Love the patterns.', 11),
    ('So unique.', 11),
    ('Looks like a lizard.', 11),
    ('So fluffy.', 12),
    ('Very friendly.', 12),
    ('Perfect pet.', 12),
    ('So small and cute.', 13),
    ('Very active.', 13),
    ('Love it as a pet.', 13),
    ('Very old creature.', 14),
    ('Moves slowly.', 14),
    ('Interesting shell.', 14),
    ('So colorful.', 15),
    ('Very lively.', 15),
    ('Love watching them.', 15);