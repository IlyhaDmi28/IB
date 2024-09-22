class LinearCongruentialGenerator {
    constructor(seed, a, c, n) {
        this.a = a;
        this.c = c;
        this.n = n;
        this.state = seed;
    }

    next() {
        this.state = (this.a * this.state + this.c) % this.n;
        return this.state;
    }
}

const a = 421;
const c = 1663;
const n = 7875;

const seed = 2;

const lcg = new LinearCongruentialGenerator(seed, a, c, n);

for (let i = 0; i < 10; i++) {
    console.log(lcg.next());
}